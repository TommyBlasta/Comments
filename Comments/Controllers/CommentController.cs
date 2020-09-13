using Comments.Factory;
using Comments.Model;
using Comments.Singleton;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Comments.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        // GET api/<CommentController>/5
        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            //todo check if the Page exists, if not return BadRequest with info
            return new JsonResult("Looks for the specific page in DB and returns its associated messages.");
        }

        // POST api/<CommentController>
        [HttpPost]
        public ActionResult Post([FromBody] string value)
        {
            //parse the body to JSon
            var recievedObject = JObject.Parse(value);
            //parse values from Json the post
            var userId = recievedObject.Value<int>("UserId");
            var postedText = recievedObject.Value<string>("Text");
            var messageType = recievedObject.Value<int>("MessageType");
            var pageId = recievedObject.Value<int>("PageId");
            //DB Access required here !!! (to get User info and Page info)
            //Would require checks for existance of the objecs
            //Now works with mock up objects
            var mockUser = new User()
            {
                Id = userId,
                CommentScore = 0,
                UserName = "user1"
            };
            var mockPage = new Page()
            {
                Id = pageId,
                Name = "testPage",
                Url = "http://mypage.com/" + pageId
            };
            BaseMessageFactory factory = null;
            //find out what type is the message and appropriate create factory
            switch (messageType)
            {
                case 1:
                    {
                        factory = new PlainMessageFactory(postedText, mockUser, mockPage);
                        break;
                    }
                case 2:
                    {
                        var multimedia = recievedObject.Value<byte[][]>("Multimedia");
                        factory = new MultiMediaMessageFactory(postedText, multimedia, mockUser, mockPage);
                        break;
                    }
                default:
                    {
                        factory = null;
                        break;
                    }
            }
            var message = factory?.GetObject();
            //Check posting behaviour and respond with the result
            switch (PostingAgent.Instance.TrackPosting(mockUser, message))
            {
                case Enums.PostingBehaviour.Spam:
                    {
                        //return TooManyRequests
                        return new StatusCodeResult(429);
                    }
                case Enums.PostingBehaviour.IncludedLinks:
                    {
                        //return NotAccepted
                        return new StatusCodeResult(406);
                    }
                case Enums.PostingBehaviour.Ok:
                    {
                        //TODO the saving to DB 
                        //return success and navigation to the page where the message was posted
                        return new ContentResult()
                        {
                            StatusCode = 201,
                            Content = string.Format("GET api/<CommentController>/{0}", mockPage.Id),
                            ContentType = "application/URI"
                        };
                    }
                default:
                    {
                        return new BadRequestResult();
                    }
            }
        }

        // DELETE api/<CommentController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            //Would access the comment and delete it from DB if the user id would be the same.
        }
    }
}
