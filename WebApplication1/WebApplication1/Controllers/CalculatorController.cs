using System.Collections.Generic;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class CalculatorController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            List<string> history = new List<string>();
            if (Session["History"] != null)
            {
                history = (List<string>)Session["History"];
            }
            ViewBag.History = history;
            return View();    
        }

        [HttpPost]
        public ActionResult Calculate(CalculatorModel model, string submitbutton)
        {
            bool status = false;
            if (submitbutton == "calculate")
            {
                if (ModelState.IsValid)
                {
                    switch (model.Operation)
                    {
                        case "+":
                            model.Result = (double)(model.Operand1 + model.Operand2);
                            break;
                        case "-":
                            model.Result = (double)(model.Operand1 - model.Operand2);
                            break;
                        case "/":
                            model.Result = (double)(model.Operand1 / model.Operand2);     
                            break;
                        case "*":
                            model.Result = (double)(model.Operand1 * model.Operand2);
                            break;
                    }

                    if (status == false)
                    {
                        ViewBag.Message = "Вычисление произведено";
                    }

                    Session["Operand1"] = model.Operand1;
                    Session["Operand2"] = model.Operand2;
                    Session["Operation"] = model.Operation;
                    Session["Result"] = model.Result;
                }
                else
                {
                    ViewBag.Message = "Введены некорректные данные";
                }

                List<string> history;
                if (Session["History"] == null)
                {
                    history = new List<string>();
                }
                else
                {
                    history = (List<string>)Session["History"];
                }

                string operationSymbol = model.Operation;
                string operationWord = "";
                switch (operationSymbol)
                {
                    case "+":
                        operationWord = "+";
                        break;
                    case "-":
                        operationWord = "-";
                        break;
                    case "/":
                        operationWord = "/";
                        break;
                    case "*":
                        operationWord = "*";
                        break;
                }

                string historyItem = $"{model.Operand1} {operationWord} {model.Operand2} = {model.Result}";
                history.Add(historyItem);

                Session["History"] = history;

                return View("Index", model);
            }

            else if (submitbutton == "clear")
            {
                model.Operand1 = 0;
                model.Operand2 = 0;
                model.Result = 0;
                
                ViewBag.Message = "Очищение выполнено";

                return View("Index", model);
            }
            else
            {
                return View("Index", model);
            }
        }

        [HttpGet]
        public ActionResult Result()
        {
            List<string> history = (List<string>)Session["History"];

            return View(history);
        }

    }
}