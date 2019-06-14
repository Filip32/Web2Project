using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApp.Models;
using WebApp.Persistence;
using WebApp.Persistence.UnitOfWork;

namespace WebApp.Controllers
{
    [RoutePrefix("api/Pricelist")]
    public class PricelistAdminController : ApiController
    {
        ApplicationDbContext dbContext = new ApplicationDbContext();
        public IUnitOfWork unitOfWork;
        private static object locka = new object();
        private static object lockb = new object();

        public PricelistAdminController(IUnitOfWork uw)
        {
            unitOfWork = uw;
        }

        [HttpGet, Route("getPricelists")]
        [Authorize(Roles = "Admin")]
        public IHttpActionResult GetPricelists()
        {
            try
            {
                List<Pricelist> pricelists = unitOfWork.PricelistRepository.GetAll().ToList();

                List<PricelistHelp> ret = new List<PricelistHelp>();
                foreach (Pricelist p in pricelists)
                {
                    ret.Add(new PricelistHelp() { Id = p.Id, FromDate = p.From.ToString("yyyy-MM-dd"), ToDate = p.To.ToString("yyyy-MM-dd"), LastUpdate = p.LastUpdate.ToString() });
                }

                return Ok(ret);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        [HttpGet, Route("getPricelist")]
        [Authorize(Roles = "Admin")]
        public IHttpActionResult GetPricelist(int id)
        {
            try
            {
                Pricelist c = unitOfWork.PricelistRepository.Get(id);
                List<PricelistItem> listaCenovnika = unitOfWork.PricelistItemRepository.Find(x => x.Pricelist_id == id).ToList();

                PricelistHelp ch = new PricelistHelp();
                ch.FromDate = c.From.ToString("yyyy-MM-dd");
                ch.ToDate = c.To.ToString("yyyy-MM-dd");
                ch.TimePrice = listaCenovnika.Where(x => x.Item_id == 1 && x.Pricelist_id == c.Id).FirstOrDefault().Price.ToString();
                ch.DailyPrice = listaCenovnika.Where(x => x.Item_id == 2 && x.Pricelist_id == c.Id).FirstOrDefault().Price.ToString();
                ch.MonthlyPrice = listaCenovnika.Where(x => x.Item_id == 3 && x.Pricelist_id == c.Id).FirstOrDefault().Price.ToString();
                ch.YearlyPrice = listaCenovnika.Where(x => x.Item_id == 4 && x.Pricelist_id == c.Id).FirstOrDefault().Price.ToString();
                ch.LastUpdate = c.LastUpdate.ToString();
                return Ok(ch);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        [HttpGet, Route("getPricelistChange")]
        [Authorize(Roles = "Admin")]
        public IHttpActionResult GetPricelistChange(int id)
        {
            try
            {
                Pricelist c = unitOfWork.PricelistRepository.Get(id);
                List<PricelistItem> listaCenovnika = unitOfWork.PricelistItemRepository.Find(x => x.Pricelist_id == id).ToList();

                PricelistHelp ch = new PricelistHelp();
                ch.FromDate = c.From.ToString("yyyy-MM-dd");
                ch.ToDate = c.To.ToString("yyyy-MM-dd");
                ch.TimePrice = listaCenovnika.Where(x => x.Item_id == 1 && x.Pricelist_id == c.Id).FirstOrDefault().Price.ToString();
                ch.DailyPrice = listaCenovnika.Where(x => x.Item_id == 2 && x.Pricelist_id == c.Id).FirstOrDefault().Price.ToString();
                ch.MonthlyPrice = listaCenovnika.Where(x => x.Item_id == 3 && x.Pricelist_id == c.Id).FirstOrDefault().Price.ToString();
                ch.YearlyPrice = listaCenovnika.Where(x => x.Item_id == 4 && x.Pricelist_id == c.Id).FirstOrDefault().Price.ToString();
                ch.LastUpdate = c.LastUpdate.ToString();

                if (DateTime.Compare(c.To, DateTime.Now) >= 0)
                    ch.Change = true;

                return Ok(ch);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        [HttpPost, Route("addPricelist")]
        [Authorize(Roles = "Admin")]
        public IHttpActionResult AddPricelist(PricelistHelp pricelist)
        {
            try
            {
                lock (locka)
                {
                    Pricelist c = new Pricelist();
                    c.From = DateTime.Parse(pricelist.FromDate);
                    c.To = DateTime.Parse(pricelist.ToDate);

                    c.LastUpdate = DateTime.Now;
                    unitOfWork.PricelistRepository.Add(c);
                    unitOfWork.Complete();

                    c = unitOfWork.PricelistRepository.Find(x => DateTime.Compare(x.From, c.From) == 0 && DateTime.Compare(x.To, c.To) == 0).ToList()[0];

                    PricelistItem cs = new PricelistItem();
                    cs.Pricelist_id = c.Id;
                    cs.Coefficients_id = unitOfWork.CoefficientRepository.Get(1).Id;
                    cs.Item_id = 1;
                    cs.Price = Decimal.Parse(pricelist.TimePrice);
                    unitOfWork.PricelistItemRepository.Add(cs);
                    unitOfWork.Complete();

                    cs = new PricelistItem();
                    cs.Pricelist_id = c.Id;
                    cs.Coefficients_id = unitOfWork.CoefficientRepository.Get(1).Id;
                    cs.Item_id = 2;
                    cs.Price = Decimal.Parse(pricelist.DailyPrice);
                    unitOfWork.PricelistItemRepository.Add(cs);
                    unitOfWork.Complete();

                    cs = new PricelistItem();
                    cs.Pricelist_id = c.Id;
                    cs.Coefficients_id = unitOfWork.CoefficientRepository.Get(1).Id;
                    cs.Item_id = 3;
                    cs.Price = Decimal.Parse(pricelist.MonthlyPrice);
                    unitOfWork.PricelistItemRepository.Add(cs);
                    unitOfWork.Complete();

                    cs = new PricelistItem();
                    cs.Pricelist_id = c.Id;
                    cs.Coefficients_id = unitOfWork.CoefficientRepository.Get(1).Id;
                    cs.Item_id = 4;
                    cs.Price = Decimal.Parse(pricelist.YearlyPrice);
                    
                    unitOfWork.PricelistItemRepository.Add(cs);
                    unitOfWork.Complete();

                    return Ok("Pricelist was added successfully.");
                }
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        [HttpPost, Route("changePricelist")]
        [Authorize(Roles = "Admin")]
        public IHttpActionResult ChangePricelist(PricelistHelp pricelist)
        {
            try
            {
                lock (lockb)
                {
                    Pricelist c = unitOfWork.PricelistRepository.Get(pricelist.Id);

                    if (pricelist.LastUpdate == c.LastUpdate.ToString())
                    {
                        if (DateTime.Compare(c.To, DateTime.Parse(pricelist.ToDate)) < 0)
                        {
                            c.To = DateTime.Parse(pricelist.ToDate);
                            c.LastUpdate = DateTime.Now;
                            unitOfWork.PricelistRepository.Update(c);
                            unitOfWork.Complete();

                            return Ok("Pricelist was changed successfully.");
                        }
                        else
                        {
                            return Ok("Date is already pass.");
                        }
                    }
                    else
                    {
                        return BadRequest("Database is changed. Please refresh page.");
                    }
                }
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }
    }
}
