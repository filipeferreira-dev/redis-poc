using pocListMerchantCache.Model.Entities;
using pocListMerchantCache.Model.RequestMessages;
using pocListMerchantCache.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pocListMerchantCache.Services
{
    public class MerchantService
    {
        MerchantRepository MerchantRepository { get; } = new MerchantRepository();

        CacheService CacheService { get; } = new CacheService();

        public async Task<Merchant> AddAsync(AddMerchantRequestMessage addMerchantReponse)
        {
            var document = new string(addMerchantReponse.Document.Where(c => char.IsDigit(c)).ToArray());
            var merchant = new Merchant() { Document = document, Name = addMerchantReponse.Name };
            await MerchantRepository.AddAsync(merchant);
            return merchant;
        }

        public async Task<IEnumerable<int>> GetAllKeysAsync()
        {
            return await MerchantRepository.GetAllKeysAsync();
        }

        public async Task<Merchant> GetAsync(int id)
        {
            var merchant = await CacheService.GetAsync<Merchant>(id.ToString());

            if (merchant != null) return merchant;

            merchant = await MerchantRepository.GetAsync(id);

            if (merchant != null) await CacheService.SetAsync(merchant.Id.ToString(), merchant);

            return merchant;
        }

        public async Task<Merchant> UpdateAsync(int id, UpdateMerchantRequestMessage updateMerchantRequestMessage)
        {
            var merchant = await MerchantRepository.GetAsync(id);

            if (merchant == null) return null;

            merchant.ChangeDocument(updateMerchantRequestMessage.Document);
            merchant.ChangeName(updateMerchantRequestMessage.Name);

            if (!await MerchantRepository.UpdateAsync(merchant)) throw new Exception($"Erro on update merchant");

            await CacheService.DelAsync(id.ToString());

            return merchant;
        }

        public async Task ClearAsync() => await CacheService.ClearAsyn();
    }
}
