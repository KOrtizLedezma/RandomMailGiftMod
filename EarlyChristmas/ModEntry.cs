using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;
using System;
using System.Collections.Generic;

public class RandomMailGiftMod : Mod
{
    string id;
    bool state = false;
    [System.Obsolete]

    public override void Entry(IModHelper helper)
    {
        var events = helper.Events;
        Helper.Content.AssetEditors.Add(new MyModMail());
        events.GameLoop.DayStarted += OnDayStarted;
    }

    private void OnDayStarted(object sender, DayStartedEventArgs e)
    {
        double luck = GetDailyLuck();
        int level = CheckIfGoodOrBadLuck(luck);
        ShowHUDMessage(level);
        id = SendLetterToPlayer(level);
    }


    private int CheckIfGoodOrBadLuck(double luck)
    {
        // 0 = very bad luck -- 1 = bad luck -- 2 = good luck -- 3 = very good luck
        if (luck < -0.05) { return 0; }
        else if (luck >= -0.05 && luck < 0) { return 1; }
        else if (luck >= 0 && luck < 0.05) { return 2; }
        else { return 3; }
    }

    private double GetDailyLuck()
    {
        string luck = Game1.player.team.sharedDailyLuck.ToString();
        double value = double.Parse(luck);
        return value;
    }

    private void ShowHUDMessage(int value)
    {
        if (value == 0)
        {
            Game1.addHUDMessage(new HUDMessage("Your biggest enemy has send you a letter", 3));
        }
        else if (value == 1)
        {
            Game1.addHUDMessage(new HUDMessage("Someone random has send you a letter", 2));
        }
        else if (value == 2)
        {
            Game1.addHUDMessage(new HUDMessage("Your High School friend has send you a letter", 2));
        }
        else
        {
            Game1.addHUDMessage(new HUDMessage("Your Mom has send you a letter", 1));
        }
    }

    public string SendLetterToPlayer(int value)
    {
        Dictionary<int, string> momDictionary = new Dictionary<int, string>();

        momDictionary.Add(1, "Mom 1");       momDictionary.Add(2, "Mom 2");      momDictionary.Add(3, "Mom 3");      momDictionary.Add(4, "Mom 4");
        momDictionary.Add(5, "Mom 5");       momDictionary.Add(6, "Mom 6");      momDictionary.Add(7, "Mom 7");      momDictionary.Add(8, "Mom 8");
        momDictionary.Add(9, "Mom 9");       momDictionary.Add(10, "Mom 10");    momDictionary.Add(11, "Mom 11");    momDictionary.Add(12, "Mom 12");
        momDictionary.Add(13, "Mom 13");     momDictionary.Add(14, "Mom 14");    momDictionary.Add(15, "Mom 15");    momDictionary.Add(16, "Mom 16");
        momDictionary.Add(17, "Mom 17");     momDictionary.Add(18, "Mom 18");    momDictionary.Add(19, "Mom 19");    momDictionary.Add(20, "Mom 20");

        Dictionary<int, string> friendDictionary = new Dictionary<int, string>();

        friendDictionary.Add(1, "Friend 1");        friendDictionary.Add(2, "Friend 2");        friendDictionary.Add(3, "Friend 3");        friendDictionary.Add(4, "Friend 4");
        friendDictionary.Add(5, "Friend 5");        friendDictionary.Add(6, "Friend 6");        friendDictionary.Add(7, "Friend 7");        friendDictionary.Add(8, "Friend 8");
        friendDictionary.Add(9, "Friend 9");        friendDictionary.Add(10, "Friend 10");      friendDictionary.Add(11, "Friend 11");      friendDictionary.Add(12, "Friend 12");
        friendDictionary.Add(13, "Friend 13");      friendDictionary.Add(14, "Friend 14");      friendDictionary.Add(15, "Friend 15");      friendDictionary.Add(16, "Friend 16");
        friendDictionary.Add(17, "Friend 17");      friendDictionary.Add(18, "Friend 18");      friendDictionary.Add(19, "Friend 19");      friendDictionary.Add(20, "Friend 20");

        Dictionary<int, string> guyDictionary = new Dictionary<int, string>();

        guyDictionary.Add(1, "Guy 1");      guyDictionary.Add(2, "Guy 2");      guyDictionary.Add(3, "Guy 3");      guyDictionary.Add(4, "Guy 4");
        guyDictionary.Add(5, "Guy 5");      guyDictionary.Add(6, "Guy 6");      guyDictionary.Add(7, "Guy 7");      guyDictionary.Add(8, "Guy 8");
        guyDictionary.Add(9, "Guy 9");      guyDictionary.Add(10, "Guy 10");    guyDictionary.Add(11, "Guy 11");    guyDictionary.Add(12, "Guy 12");
        guyDictionary.Add(13, "Guy 13");    guyDictionary.Add(14, "Guy 14");    guyDictionary.Add(15, "Guy 15");    guyDictionary.Add(16, "Guy 16");
        guyDictionary.Add(17, "Guy 17");    guyDictionary.Add(18, "Guy 18");    guyDictionary.Add(19, "Guy 19");    guyDictionary.Add(20, "Guy 20");

        Random random = new Random();
        if (value == 3)
        {
            int randomNumber = random.Next(1, 21);
            Game1.player.mailbox.Add(momDictionary[randomNumber]);
            return momDictionary[randomNumber];
        }
        else if (value == 2)
        {
            int randomNumber = random.Next(1, 21);
            Game1.player.mailbox.Add(friendDictionary[randomNumber]);
            return friendDictionary[randomNumber];
        }
        else if (value == 1)
        {
            int randomNumber = random.Next(1, 21);
            Game1.player.mailbox.Add(guyDictionary[randomNumber]);
            return guyDictionary[randomNumber];
        }
        else
        {
            Game1.player.mailbox.Add("Hate");
            return "Hate";
        }
    }
}

[System.Obsolete]
public class MyModMail : IAssetEditor
{
    public bool CanEdit<T>(IAssetInfo asset)
    {
        return asset.AssetNameEquals("Data\\mail");
    }

    public void Edit<T>(IAssetData asset)
    {
        var data = asset.AsDictionary<string, string>().Data;

        data["Mom 1"] = "Dear @,^Just a quick note to say I love you more than words can express. " +
            "Your smile brightens my day. Keep shining, my dear.^Love,^Mom < %item object 221 6 %%";

        data["Mom 2"] = "Hey there,^Remember, life is an adventure. Embrace every moment, learn from every experience, " +
            "and know that I'm always here cheering you on.^Love,^Mom  < %item object 215 6 %%";

        data["Mom 3"] = "To my amazing son,^Your kindness and strength inspire me every day. Never forget " +
            "how much you are loved.^Love,^Mom  < %item object 211 6 %%";

        data["Mom 4"] = "Hi sweetheart,^Sending you a hug through this letter. I believe in you and your dreams. " +
            "Chase them fearlessly.^Love,^Mom < %item object 732 1 %%";

        data["Mom 5"] = "My dearest,^Life may have its ups and downs, but you have the resilience to navigate through " +
            "it all. Always by your side.^Love,^Mom < %item object 803 3 %%";

        data["Mom 6"] = "Dear @,^Your laughter is my favorite sound. Keep finding joy in the little things." +
            "^Love,^Mom < %item object 787 5 %%";

        data["Mom 7"] = "Hello love,^Just a reminder that you are strong, capable, and loved. Your potential is limitless." +
            "^Love,^Mom < %item object 527 1 %%";

        data["Mom 8"] = "To my son,^Your heart is pure, and your spirit is strong. The world is a better place " +
            "with you in it.^Love,^Mom < %item object 337 10 %%";

        data["Mom 9"] = "Hey sunshine,^Your determination is admirable. Don't forget to take breaks and enjoy the journey." +
            "^Love,^Mom < %item object 337 10 %%";

        data["Mom 10"] = "Dear @,^Life is a book, and you are the author. Write a story that fills your heart with joy." +
            "^Love,^Mom < %item object 336 10 %%";

        data["Mom 11"] = "Sweetheart,^In the dance of life, never forget that I'll be your biggest fan, cheering from the sidelines." +
            "< %item object 797 10 %%^Love,^Mom";

        data["Mom 12"] = "My dear,^Your kindness and compassion make my heart swell with pride. Keep spreading love in the world." +
            "^Love,^Mom < %item object 349 10 %%";

        data["Mom 13"] = "Hi love,^You're growing into an incredible person. Cherish every moment, and know that I'm here for you." +
            "^Love,^Mom < %item object 373 10 %%";

        data["Mom 14"] = "To my son,^Life's journey may be challenging at times, but your strength will guide you through. I believe " +
            "in you.^Love,^Mom < %item object 595 10 %%";

        data["Mom 15"] = "Dear @,^Your achievements, big or small, make my heart swell with pride. Keep reaching " +
            "for the stars.^Love,^Mom < %item object 289 10 %%";

        data["Mom 16"] = "Hey champ,^Life is an open road. Enjoy the ride, and remember that I'm here to support you every step " +
            "of the way.^Love,^Mom < %item object 107 10 %%";

        data["Mom 17"] = "My love,^Your uniqueness is your superpower. Embrace it, and let it shine in everything you do." +
            "^Love,^Mom < %item object 268 10 %%";

        data["Mom 18"] = "Dear @,^You are a gift to the world. Never underestimate the positive impact you can " +
            "make.^Love,^Mom < %item object 447 10 %%";

        data["Mom 19"] = "Hello sweetheart,^Your smile is contagious, and your heart is pure gold. Keep spreading " +
            "joy wherever you go.^Love,^Mom < %item object 432 10 %%";

        data["Mom 20"] = "To my dear son,^Life is a canvas, and you are the artist. Paint it with colors that make " +
            "your heart sing.^Love,^Mom < %item object 460 10 %% ";

        data["Friend 1"] = "Hey Buddy,^Just wanted to remind you that you're awesome. Your friendship means the world to me. " +
            "Cheers to many more adventures together!^Cheers,^ Your Friend %item object 472 50 %%";

        data["Friend 2"] = "Dear Your Friend,^Thinking of you and grateful for our friendship. Life is better with you by my side. " +
            "Let's catch up soon!^Much love,^Your Friend %item object 473 50 %%";

        data["Friend 3"] = "Hi Pal,^Your friendship is like a bright light on my darkest days. Thank you for being you. " +
            "Coffee date soon?^Hugs,^Your Friend %item object 474 50 %%";

        data["Friend 4"] = "To My Partner in Crime,^From silly jokes to deep talks, our friendship is my favorite. Thanks for being my ride-or-die. " +
            "Pizza night this weekend?^Yours,^Your Friend %item object 475 50 %%";

        data["Friend 5"] = "Hello Friend,^Sending good vibes your way. Remember, you've got this! Can't wait for our next hangout." +
            "^Take care,^Your Friend %item object 476 50 %%";

        data["Friend 6"] = "Dear Your Friend,^Just wanted to say thanks for being the amazing person you are. Your positivity is contagious. " +
            "Let's plan a movie night!^Gratefully,^Your Friend %item object 477 50 %%";

        data["Friend 7"] = "Hi There,^Friendship isn't about being inseparable but about being separated and nothing changing. Miss you and can't wait " +
            "for our next reunion.^Always,^Your Friend %item object 478 50 %%";

        data["Friend 8"] = "Hey Champ,^Your resilience is inspiring. If you ever need someone to talk to, I'm here. Ice cream and heart-to-heart soon?" +
            "^Supportively,^Your Friend %item object 479 50 %%";

        data["Friend 9"] = "To My Favorite Human,^Life's journey is better with you as my co-pilot. Thanks for being there through thick and thin. " +
            "Let's plan a road trip!^Your Sidekick,^Your Friend %item object 480 50 %%";

        data["Friend 10"] = "Dear Your Friend,^Your laughter is my favorite soundtrack. Thanks for the joy you bring into my life. Virtual game " +
            "night this Friday?^Smiling,^Your Friend %item object 481 50 %%";

        data["Friend 11"] = "Hi Sunshine,^Your positive energy is like a ray of sunlight. Grateful to have you as a friend. Let's plan a picnic soon?" +
            "^Brightly,^Your Friend %item object 482 50 %%";

        data["Friend 12"] = "To My Confidant,^Your friendship is my safe haven. Thanks for always being there to listen. Coffee and catch-up this " +
            "Saturday?^Always,^Your Friend %item object 483 50 %%";

        data["Friend 13"] = "Dear Your Friend,^In this crazy journey called life, I'm glad to have you as my fellow explorer. Let's make more memories " +
            "together.^Adventurously,^Your Friend %item object 484 50 %%";

        data["Friend 14"] = "Hey Rockstar,^Your achievements make me proud to call you my friend. Keep shining! Celebration dinner next week?^Proudly," +
            "^Your Friend %item object 485 50 %%";

        data["Friend 15"] = "Hi Soul Sister/Brother,^Your friendship is a treasure. Thanks for understanding me like no one else. Spa day on the horizon?^" +
            "Soulfully,^Your Friend %item object 486 50 %%";

        data["Friend 16"] = "Dear Your Friend,^You're the peanut butter to my jelly. Thanks for sticking by me. Movie marathon at my place soon?^" +
            "Yours Truly,^Your Friend %item object 487 50 %%";

        data["Friend 17"] = "Hey Partner in Crime,^Life's an adventure, and I'm glad to have you as my co-adventurer. Let's plan something spontaneous this " +
            "weekend!^Spontaneously,^Your Friend %item object 488 50 %%";

        data["Friend 18"] = "Hi Comrade,^Your loyalty is a gift. Thanks for being a true friend. Barbecue night at my place?^Grilling,^" +
            "Your Friend %item object 489 50 %%";

        data["Friend 19"] = "To My Forever Friend,^Your friendship is timeless. Let's create more memories together. Beach day in the near future?" +
            "^Timelessly,^ Your Friend %item object 490 50 %%";

        data["Friend 20"] = "Hello Buddy,^Just a reminder that you're irreplaceable. Grateful for your friendship. Let's grab a cup of coffee soon?^" +
            "Appreciatively,^Your Friend %item object 491 50 %%";

        data["Guy 1"] = "Hey Man,^Just wanted to drop a quick note to say what's up. Hope life's treating you well. " +
            "Be awesome!^Cheers,^Your Buddy %item object 388 50 %%";

        data["Guy 2"] = "Yo Dude,^Thinking of you and sending good vibes. Let's catch up soon over a cold one. " +
            "Stay rad!^Cheers,^Your Bro %item object 382 50 %%";

        data["Guy 3"] = "Hi Pal,^Life's a journey, and I'm glad to have you on the ride. Keep rocking those challenges. " +
            "Be legendary!^Cheers,^Your Mate %item object 390 50 %%";

        data["Guy 4"] = "Sup Buddy,^From one dude to another, just a shoutout to remind you that you're awesome." +
            " Keep slaying!^Cheers,^Your Comrade %item object 388 50 %%";

        data["Guy 5"] = "Hey Dude,^Hope this message adds a smile to your day. If you need a wingman for anything, " +
            "I got your back!^Cheers,^Your Homie %item object 382 50 %%";

        data["Guy 6"] = "Hi Bro,^Life's too short not to appreciate awesome dudes like you. Keep doing your thing, " +
            "my man!^Cheers,^Your Amigo %item object 390 50 %%";

        data["Guy 7"] = "What's up Dude,^Just wanted to say you're a solid guy. Let's plan a hangout soon and grab " +
            "some burgers.^Cheers,^Your Pal %item object 388 50 %%";

        data["Guy 8"] = "Hey Man,^Life can get crazy, but remember, you got this! If you ever need to vent, I'm here. " +
            "Stay strong!^Cheers,^Your Support %item object 382 50 %%";

        data["Guy 9"] = "Yo Bro,^Sending fist bumps your way. Your vibe is contagious, and the world needs more dudes " +
            "like you.^Cheers,^Your Cheerleader %item object 390 50 %%";

        data["Guy 10"] = "Hey Dude,^Just wanted to remind you that you're a legend in the making. Keep chasing those " +
            "dreams!^Cheers,^Your Wingman %item object 388 50 %%";

        data["Guy 11"] = "Sup Man,^Life's an adventure, and I'm glad to have you as a fellow explorer. Let's make some " +
            "epic memories!^Cheers,^Your Adventurer %item object 382 50 %%";

        data["Guy 12"] = "Hey Bro,^Your humor is top-notch. Thanks for the laughs and being an awesome friend. Let's plan " +
            "a game night!^Cheers,^Your Jester %item object 390 50 %%";

        data["Guy 13"] = "What's up Dude,^Just checking in to say you're not alone in this journey. If you need a chat, " +
            "I'm a message away.^Cheers,^Your Listener %item object 388 50 %%";

        data["Guy 14"] = "Hi Buddy,^Life's a rollercoaster, but with you as a friend, it's a thrilling ride. Let's grab " +
            "some adrenaline soon!^Cheers,^Your Rollercoaster Mate %item object 382 50 %%";

        data["Guy 15"] = "Hey Man,^Your determination is inspiring. Keep pushing boundaries and breaking limits. The world" +
            " is yours!^Cheers,^Your Trailblazer %item object 390 50 %%";

        data["Guy 16"] = "Sup Bro,^Friendship is like a good game. Thanks for being on my team. Victory dance soon?^" +
            "Cheers,^Your Teammate %item object 382 50 %%";

        data["Guy 17"] = "What's up Dude,^Life's too short for negativity. Keep the positive vibes flowing. You're making a " +
            "difference!^Cheers,^Your Positivity Buddy %item object 390 50 %%";

        data["Guy 18"] = "Hi Bro,^Your loyalty is a rare gem. Thanks for being a true friend. Let's plan a barbecue night!" +
            "^Cheers,^Your Grillmaster %item object 382 50 %%";

        data["Guy 19"] = "Hey Man,^Just wanted to express gratitude for your friendship. You're a stand-up guy, and I appreciate " +
            "that.^Cheers,^Your Grateful Pal %item object 388 50 %%";

        data["Guy 20"] = "Sup Dude,^Life's an open road, and I'm glad to be cruising it with you. Let's plan a road trip adventure!" +
            "^Cheers,^Your Road Trip Buddy %item object 382 50 %%";

        data["Hate"] = "Sup Dude,^I hate you and I hope you tripped with your own feet" +
            "^Cheers,^ %item object 168 999 %%";
    }
}
