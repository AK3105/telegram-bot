
#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebServer.Models;
using System.Net;

namespace WebServer.Controllers
{

    [Route("api/register")] //зарегаться
    [ApiController]
    public class Register : ControllerBase
    {
        private readonly UserContext _context;

        public Register(UserContext context)
        {
            _context = context;
        }

        // POST: api/Register
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<User>> PostRegister(User user)
        {

            if (_context.Users.Any(e => e.Tg_id == user.Tg_id)) //проверка на существующий тг айди
                return Content("{\"response\":\"ex_tg_id\"}", "application/json");

            if (_context.Users.Any(e => e.Name == user.Name)) //проверка на существующий логин
                return Content("{\"response\":\"ex_login\"}", "application/json");

            _context.Users.Add(user); //если такого юзера нет то добавляем
            await _context.SaveChangesAsync(); 

            return Content("{\"response\":\"true\"}", "application/json");
        }


    }

    [Route("api/sendcode")] //отправить код
    [ApiController]
    public class SendCode : ControllerBase
    {
        private readonly UserContext _context;

        public SendCode(UserContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<User>> PostSendCode(User user)
        {

            if (_context.Users.Any(e => e.Name == user.Name & e.Password == user.Password)) 
            {
                User cur_user = await _context.Users.FirstOrDefaultAsync(e => e.Name == user.Name & e.Password == user.Password);

                Random rnd = new Random();
                int code = rnd.Next(1000, 9999);

                cur_user.Code = code;

                await _context.SaveChangesAsync();

                var url = $"https://api.telegram.org/bot2079170564:AAGkfdXLDF3CVg-tMjH-oMIXqxkC7F_nJD8/sendMessage?chat_id={cur_user.Tg_id}&text={code}";
                
                var request = WebRequest.Create(url); //запрос на отправку кода ботом
                request.Method = "GET";
                try
                {
                    using var webResponse = (HttpWebResponse)request.GetResponse();
                    int status_code = (int)webResponse.StatusCode;
                }
                catch (WebException we)
                {
                    int status_code = (int)((HttpWebResponse)we.Response).StatusCode;
                    return Content("{\"response\":\"ex_tg_api\"}", "application/json");
                }



                return Content("{\"response\":\"true\"}", "application/json");
            }
            else
                return Content("{\"response\":\"ex_login_pass\"}", "application/json");

        }
    }

    [Route("api/first_login")] //запрос первой авторизации
    [ApiController]
    public class FirstLogin : ControllerBase
    {
        private readonly UserContext _context;

        public FirstLogin(UserContext context)
        {
            _context = context;
        }

        // POST: api/Register
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<User>> PostLogin(User user)
        {

            if (_context.Users.Any(e => e.Name == user.Name & e.Password == user.Password & e.Code == user.Code))
            {
                User cur_user = await _context.Users.FirstOrDefaultAsync(e => e.Name == user.Name & e.Password == user.Password & e.Code == user.Code);
                await _context.SaveChangesAsync();

                return Content("{\"response\":\"true\"}", "application/json");
            }
            else
                return Content("{\"response\":\"ex_login_pass\"}", "application/json");
        }
    }

   

}

