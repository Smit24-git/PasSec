using AutoMapper;
using MediatR;
using Microsoft.Extensions.Configuration;
using PasSecWebApi.Repositories.Contracts.Infrastructure;
using PasSecWebApi.Repositories.Contracts.Persistence.Vaults;
using PasSecWebApi.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasSecWebApi.Application.Features.VaultStorageSecurityQAs.Queries.GetSecurityQAsByStorageId
{
    public class GetSecurityQAsByStorageIdQueryHandler : IRequestHandler<
        GetSecurityQAsByStorageIdQuery,
        GetSecurityQAsByStorageIdQueryResponse>
    {
        private readonly string _key;
        private readonly IVaultStorageKeySecurityQARepository _repo;
        private readonly IMapper _mapper;
        private readonly IDataDecryptor _decryptor;

        public GetSecurityQAsByStorageIdQueryHandler(
            IConfiguration configuration,
            IVaultStorageKeySecurityQARepository repo, 
            IMapper mapper,
            IDataDecryptor dataDecryptor)
        {
            _key = configuration["ApiSettings:Secret"]!;
            _repo = repo;
            _mapper = mapper;
            _decryptor = dataDecryptor;
        }

        public async Task<GetSecurityQAsByStorageIdQueryResponse> Handle(GetSecurityQAsByStorageIdQuery request, CancellationToken cancellationToken)
        {
            var res = new GetSecurityQAsByStorageIdQueryResponse();
            var encryptionKey = string.IsNullOrEmpty(request.UserKey) ? _key : request.UserKey;
            var qas = await _repo.ListAllSequrityQAbyFilterAsync((v) => v.VaultStorageKeyId == new Guid(request.VaultStorageKeyId));
            foreach (var qa in qas)
            {
                qa.Question = _decryptor.DecryptCipher(encryptionKey, qa.IV, qa.Question);
                qa.Answer = _decryptor.DecryptCipher(encryptionKey,qa.IV, qa.Answer);
            }
            res.SecurityQAs = _mapper.Map<List<VaultStorageKeySecurityQADto>>(qas);
            return res;
        }
    }
}
