 _____                                      _                       _   _                ___  ______ _____ 
/  __ \                                    | |                     | | (_)              / _ \ | ___ \_   _|
| /  \/ ___  _ __ ___  _ __ ___   ___ _ __ | |_ ___   ___  ___  ___| |_ _  ___  _ __   / /_\ \| |_/ / | |  
| |    / _ \| '_ ` _ \| '_ ` _ \ / _ \ '_ \| __/ __| / __|/ _ \/ __| __| |/ _ \| '_ \  |  _  ||  __/  | |  
| \__/\ (_) | | | | | | | | | | |  __/ | | | |_\__ \ \__ \  __/ (__| |_| | (_) | | | | | | | || |    _| |_ 
 \____/\___/|_| |_| |_|_| |_| |_|\___|_| |_|\__|___/ |___/\___|\___|\__|_|\___/|_| |_| \_| |_/\_|    \___/ 

Základní API pro zpracování komentářů.

Zadání jsem si představil jako stránku s články, pod které lze psát uživatelské komentáře. Uživatelé mají nejspíš nějaký login (jsou uložení v DB a mají Id), ale ten jsem neimplementoval.
Objekty v Modelu jsou:

Page - stránka, pro kterou lze zobrazit komentáře.
User - uživatel, který může přidávat komentáře.
Message - komentář, který má jednoho svého Usera a Page, kteří definují kde a kým byl přidán.

Pro API jsem neimplementoval DB. Práce s ní by byla pro jednotlivé volání:

GET - client si žádá komentáře pro nějaký článek, server vrátí JSon s těmito komentáři, tedy by se z tabulky Messages filtrovalo dle žádané Page.
POST - client chce poslat komentář. Server zkontroluje zaslaná data a pokud jsou v pořádku (neobsahují linky a nejedná se o spam), tak je uloží do DB v tabulce Messages s odpovídající Page
		odkud user/client postoval a s odpovídajícím Id usera.
DELETE (??) - potenciálně by client/user mohly mazat komentáře. Zde by server podle zadaného Id vyhledal Message v DB a smazal jí.

Pro POST a DELETE dává smysl, aby v odpovědi byla navigace pro refresh obsahu na stránce dle změn, které server provedl. Zatím by POST měl v response vrátit GET, ve kterém je nový komentář obsažený.

Použil jsem tři design patterns, jmenovitě Singleton (složka Singleton), Factory (složka Factory) a Prototype (složka Model/Interfaces - IClonable a implementace v Messages).

Singleton funguje jako kontrola postování uživatelů. Uchovává si globálně to, jací uživatelé, a kdy zadali nějaký komentář. 
Při pokusu o přídání nového komentáře kontroluje zda nejde o spam (více než jeden komentář v jedné minutě) nebo jestli komentář neobsahuje linky, a zareaguje tím, že povolí nebo nepovolí uložení komentáře.
Objekt vrací enum, který odpovídá možnostem/chybám, které mohou nastat. Pro rozšíření o větší množství chyb by bylo záhodno jej změnit na flag enum, aby mohl obsahovat více chyb zároveň.

Factory slouží pro vytváření messages objektů, se kterými aplikace funguje. Předpoklad je takový, že by se v budoucnu mohly přidat další typy zpráv, se kterými by se mohlo pracovat v 
abstraktní třídě BaseMessageFactory. Messages jsou nastíněny dvou typů: Plain (pouze textová část) a Multimedia (obsahující byte[] reprezentace nějakých multimediálních obsahů). Toto by se 
myslím dalo implementovat i pomocí Builder patternu, ale možná až když by přibylo více argumentů při konstrukci. Factories mají i zděděnou metodu pro generování stringových guidů.

Prototype slouží pro klonování objektů. Samotná funkčnost není v implementaci využita, ale pro budoucí features, které mě napadaly dával pattern smysl. Pokud by chtěl uživatel například editovat 
komentář, bylo by možné na server vytvořit kopii se kterou by se při editaci pracovalo. Tuto funkčnost by bylo možné využít i např. pro uvádění citací komantářu v komentářích, kdy by se citace
chovala jako kopie komentáře s navigací na originál.




