using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using serveris.Controllers;
using serveris.Models;

namespace serveris.Services
{
    public class BetService
    {
        /*public static List<BetItem> GetAll()
        {
            return BetItemController.test();
        }*/
        public readonly IServiceCollection servicess;

        public static List<GameItem> GetAll()
        {
            //servicess.db
            return GameItemController.test();
        }
    }
}
