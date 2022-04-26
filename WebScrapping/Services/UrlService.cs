using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebScrapping.Models;

namespace WebScrapping.Services
{
    public class UrlService
    {
        private readonly ApplicationDBContext _context;

        public UrlService(ApplicationDBContext context)
        {
            _context = context;
        }

        private void ValidateUrl(UrlModel url)
        {
            if (string.IsNullOrEmpty(url.Product)) throw new Exception("El producto es requerido");
            if (string.IsNullOrEmpty(url.Selector)) throw new Exception("El selector es requerido");
            if (string.IsNullOrEmpty(url.UrlLink)) throw new Exception("El link es requerido");
        }

        public async Task<UrlModel> SaveUrl(UrlModel url)
        {
            try
            {
                ValidateUrl(url);
                _context.UrlModel.Add(url);
                await _context.SaveChangesAsync();
                return url;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<UrlModel>> GetAll()
        {
            try
            {
                return await _context.UrlModel.Where(x => x.FindSuccess == false).ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<UrlModel> GetUrlById(string id)
        {
            try
            {   
                var url = await _context.UrlModel.FindAsync(new Guid(id));
                if (url is null) throw new Exception("El producto no ha sido encontrado.");
                return url;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<UrlModel> UpdateUrl(UrlModel url)
        {
            try
            {
                ValidateUrl(url);
                _context.Update(url);
                await _context.SaveChangesAsync();
                return url;
            }
            catch (Exception ex)
            {
                throw ex;
            }
           
        }

        public async Task Delete(string id)
        {
            try
            {
                var url = await GetUrlById(id);
                _context.UrlModel.Remove(url);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<UrlModel> FindSuccessUpdate(string id)
        {
            try
            {
                var url = await GetUrlById(id);
                url.FindSuccess = true;
                await UpdateUrl(url);
                return url;
            }
            catch (Exception ex)
            {
                throw ex;
            }
           
        }
        public async Task<UrlModel> ImgUpdate(string id, byte[] img)
        {
            try
            {
                var url = await GetUrlById(id);
                url.Img = img;
                await UpdateUrl(url);
                return url;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
