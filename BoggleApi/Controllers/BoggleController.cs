using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using BoggleApi.Models;
using BoggleApi.Services;
using System.Linq;
using Microsoft.Office.Interop.Word;


namespace BoggleApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BoggleController : ControllerBase
    {

        private readonly IBoggleService _boggleService;
        private static Application micWord;
        private static Language language;

        

        public BoggleController(IBoggleService boggleService)
        {
            _boggleService = boggleService ?? throw new ArgumentNullException(nameof(boggleService));
            if (micWord == null)
            {
                micWord = new Application();
                language = micWord.Languages[WdLanguageID.wdDutch];
            }
        }

       

        

        [HttpGet("getbogglebox")]
        public BoggleBox GetBoggleBox() => _boggleService.GetBoggleBox();

        [HttpGet("getbogglebox/{boggleBoxId}")]
        public ActionResult<BoggleBox> GetBoggleBox(Guid boggleBoxId)
        {
            var boggleBox = _boggleService.GetBoggleBox(boggleBoxId);

            if (boggleBox == null)
            {
                return NotFound();
            }

            return boggleBox;
        }

        [HttpGet("isvalidword/{boggleBoxId}/{word}")]
        public int IsValidWord(Guid boggleBoxId, string word)
        {
            var boggleBox = _boggleService.GetBoggleBox(boggleBoxId);

            
             
            //bool isValidWord = false;

            if (_boggleService.CheckWordPresent(boggleBox, word) && micWord.CheckSpelling(word.ToLower(), MainDictionary: language.Name) && word.Length > 2)
            {
                int score = ScoreWord(word);
                return score;
            }
            else
            {
                return 0;
            }

            //if (word.Length >= 3)
            //{
            //    isValidWord = wordApp.CheckSpelling(word.ToLower(), MainDictionary: language.Name);
            //}

            
        }


        //[HttpGet("scoreword/{word}")]
        public int ScoreWord(string word)
        {
            

            if (word.Length == 3 || word.Length == 4)
            {
                return 1;
            }
            else if (word.Length == 5)
            {
                return 2;
            }
            else if (word.Length == 6)
            {
                return 3;
            }
            else if (word.Length == 7)
            {
                return 5;
            }
            else if (word.Length >= 8)
            {
                return 11;
            }

            return 0;
        }

    }
}

    

