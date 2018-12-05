using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using serveris.Models;
using serveris.Services;
using Microsoft.AspNetCore.Authorization;

namespace serveris.Controllers
{
    [Authorize]
    [Route("api/bet")]
    [ApiController]
    public class BetItemController : ControllerBase
    {
        public static BetItemContext _context;

        public BetItemController(BetItemContext context)//, GameItemContext context1, WinnerContext context2)
        {
            _context = context;
            //_contextGame = context1;
            //_contextWin = context2;

            if (_context.betItems.Count() == 0)
            {
                _context.betItems.Add(new BetItem { PersonId = 999 });
                _context.betItems.Add(new BetItem { GameId = 2, PersonId = 1, ChosenId = 1, BetMoney = 1000, HasWon = 0 });
                _context.SaveChanges();
            }

            if (_context.Winners.Count() == 0)
            {
                _context.Winners.Add(new Winner { GameId = 1, Win = "first" });
                _context.SaveChanges();
            }
        }

        //GET
        [HttpGet]
        public ActionResult<List<BetItem>> GetAll()
        {
            //also check if any games are finished
            foreach (BetItem bet in _context.betItems.ToList())
            {
                foreach (GameItem game in _context.GameItems.ToList())
                {
                    int gameid = Convert.ToInt32(game.Id);
                    if (bet.GameId == gameid)
                    {
                        if (bet.HasWon == 0)
                        {
                            if (game.IsComplete)
                            {
                                double price = 0;
                                var won = 0;
                                if (bet.ChosenId == 1 && game.Winner == "1")
                                {
                                    won = 1;
                                    price = bet.BetMoney * game.Firstkof;
                                }
                                else if (bet.ChosenId == 2 && game.Winner == "2")
                                {
                                    won = 1;
                                    price = bet.BetMoney * game.Secondkof;
                                }
                                else
                                    won = 2;

                                //save that bet finished
                                bet.HasWon = won;
                                //_context.betItems.Update(bet);

                                //save new balance to user
                                foreach (UserItem user in _context.UserItems.ToList())
                                {
                                    if(user.Id == bet.PersonId && won == 1)
                                    {
                                        user.AccountBalance = user.AccountBalance + price;
                                        user.GamesWon = user.GamesWon + 1;
                                        _context.UserItems.Update(user);
                                        _context.SaveChanges();
                                    }
                                    if (user.Id == bet.PersonId && won == 2)
                                    {
                                        user.GamesLost = user.GamesLost + 1;
                                        _context.UserItems.Update(user);
                                        _context.SaveChanges();
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return _context.betItems.ToList();
        }

        [HttpGet("{id}", Name = "GetBet")]
        public ActionResult<BetItem> GetById(long id)
        {
            var item = _context.betItems.Find(id);
            if (item == null)
            {
                return NotFound();
            }
            return item;
        }

        //POST
        [HttpPost]
        public IActionResult Create(BetItem item)
        {
            //get acc balance
            double accountBalance = 0;
            UserItem useris = new UserItem();
            foreach (UserItem user in _context.UserItems.ToList())
            {
                if (user.Id == item.PersonId)
                {
                    accountBalance = user.AccountBalance;
                    useris = user;
                }
                else
                    return NoContent();
            }

            //check balance
            if (accountBalance < item.BetMoney)
                return NoContent();

            //Check if game finished, if yes, return
            foreach (GameItem game in _context.GameItems.ToList())
                if (game.Id == item.GameId)
                    if (game.IsComplete)
                        return NoContent();

            //if all good, minus the money
            useris.AccountBalance = useris.AccountBalance - item.BetMoney;
            _context.UserItems.Update(useris);

            _context.betItems.Add(item);
            _context.SaveChanges();

            return CreatedAtRoute("GetBet", new { id = item.Id }, item);
        }

        //UPDATE
        [HttpPut("{id}")]
        public IActionResult Update(long id, BetItem item)
        {
            var todo = _context.betItems.Find(id);
            if (todo == null)
            {
                return NotFound();
            }

            todo.BetMoney = item.BetMoney;
            todo.ChosenId = item.ChosenId;
            todo.GameId = item.GameId;
            todo.HasWon = item.HasWon;
            todo.PersonId = item.PersonId;
            todo.PossibleWinMoney = item.PossibleWinMoney;

            _context.betItems.Update(todo);
            _context.SaveChanges();
            return NoContent();
        }

        //DELETE
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var todo = _context.betItems.Find(id);
            if (todo == null)
            {
                return NotFound();
            }

            _context.betItems.Remove(todo);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
