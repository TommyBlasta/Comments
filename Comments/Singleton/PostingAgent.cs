using Comments.Enums;
using Comments.Model;
using Comments.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Comments.Singleton
{
    /// <summary>
    /// Tracks timestamps of user posts and the behaviour of the poster.
    /// </summary>
    public class PostingAgent
    {
        private Dictionary<User, DateTime> Postings { get; set; }
        public static PostingAgent Instance { get; } = new PostingAgent();
        private PostingAgent() { }
        public PostingBehaviour TrackPosting(User poster, IMessage post)
        {
            //checks if the last post was more than a minute ago
            if (Postings.ContainsKey(poster) && DateTime.Now.Subtract(Postings.GetValueOrDefault(poster)) < TimeSpan.FromMinutes(1))
            {
                return PostingBehaviour.Spam;
            }
            //checks if posted text doesnt contain links
            else if (CheckForUrls(post.Text))
            {
                return PostingBehaviour.IncludedLinks;
            }
            else if(Postings.ContainsKey(poster))
            {
                Postings[poster] = DateTime.Now;
                return PostingBehaviour.Ok;
            }
            else
            {
                Postings.Add(poster, DateTime.Now);
                return PostingBehaviour.Ok;
            }
        }
        /// <summary>
        /// Checks whether the string contains an URL in leading HTTP/HTTPS format
        /// </summary>
        /// <param name="toCheck">The string to check for URLs.</param>
        /// <returns>True if there is a URL in the string, else false.</returns>
        private bool CheckForUrls(string toCheck)
        {
            Regex urlRegex = new Regex(@"https?:\/\/(www\.)?[-a-zA-Z0-9@:%._\+~#=]{1,256}\.[a-zA-Z0-9()]{1,6}\b([-a-zA-Z0-9()@:%_\+.~#?&//=]*)");
            return urlRegex.IsMatch(toCheck);
        }
    }
}

