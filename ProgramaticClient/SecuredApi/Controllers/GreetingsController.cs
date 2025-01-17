﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class GreetingsController : ControllerBase
    {
        private static readonly Random Rng 
            = new Random();

        // List from: https://stackoverflow.com/a/55950621
        private static readonly string[] Greetings 
            = {
                "hi", "hello", "hey", "helloo", "hellooo", "g morining", "gmorning", "good morning", "morning", "good day",
                "good afternoon", "good evening", "greetings", "greeting", "good to see you", "its good seeing you",
                "how are you", "how're you", "how are you doing", "how ya doin'", "how ya doin", "how is everything",
                "how is everything going", "how's everything going", "how is you", "how's you", "how are things",
                "how're things", "how is it going", "how's it going", "how's it goin'", "how's it goin",
                "how is life been treating you", "how's life been treating you", "how have you been", "how've you been",
                "what is up", "what's up", "what is cracking", "what's cracking", "what is good", "what's good",
                "what is happening", "what's happening", "what is new", "what's new", "what is neww", "gday", "howdy"
            };

        [HttpGet]
        public string Get()
        {
            var idx = Rng.Next(0, Greetings.Length - 1);
            return Greetings[idx];
        }
    }
}
