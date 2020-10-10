﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Models;
using IdentityServer4.Stores;
using Volo.Abp.IdentityServer.ApiResources;
using Volo.Abp.IdentityServer.IdentityResources;
using Volo.Abp.ObjectMapping;
using ApiResource = IdentityServer4.Models.ApiResource;
using IdentityResource = Volo.Abp.IdentityServer.IdentityResources.IdentityResource;

namespace Volo.Abp.IdentityServer
{
    public class ResourceStore : IResourceStore
    {
        protected IIdentityResourceRepository IdentityResourceRepository { get; }
        protected IApiResourceRepository ApiResourceRepository { get; }
        protected IObjectMapper<AbpIdentityServerDomainModule> ObjectMapper { get; }

        public ResourceStore(
            IIdentityResourceRepository identityResourceRepository,
            IObjectMapper<AbpIdentityServerDomainModule> objectMapper,
            IApiResourceRepository apiResourceRepository)
        {
            IdentityResourceRepository = identityResourceRepository;
            ObjectMapper = objectMapper;
            ApiResourceRepository = apiResourceRepository;
        }

        //public virtual async Task<IEnumerable<IdentityServer4.Models.IdentityResource>> FindIdentityResourcesByScopeAsync(IEnumerable<string> scopeNames)
        //{
        //    var resource = await IdentityResourceRepository.GetListByScopesAsync(scopeNames.ToArray(), includeDetails: true);
        //    return ObjectMapper.Map<List<IdentityResource>, List<IdentityServer4.Models.IdentityResource>>(resource);
        //}

        //public virtual async Task<IEnumerable<ApiResource>> FindApiResourcesByScopeAsync(IEnumerable<string> scopeNames)
        //{
        //    var resources = await ApiResourceRepository.GetListByScopesAsync(scopeNames.ToArray(), includeDetails: true);
        //    return resources.Select(x => ObjectMapper.Map<ApiResources.ApiResource, ApiResource>(x));
        //}

        //public virtual async Task<ApiResource> FindApiResourceAsync(string name)
        //{
        //    var resource = await ApiResourceRepository.FindByNameAsync(name);
        //    return ObjectMapper.Map<ApiResources.ApiResource, ApiResource>(resource);
        //}

        public virtual async Task<Resources> GetAllResourcesAsync()
        {
            var identityResources = await IdentityResourceRepository.GetListAsync(includeDetails: true);
            var apiResources = await ApiResourceRepository.GetListAsync(includeDetails: true);
            var apiScopes = apiResources.SelectMany(t => t.Scopes).ToList();

            return new Resources(
                ObjectMapper.Map<List<IdentityResource>, IdentityServer4.Models.IdentityResource[]>(identityResources),
                ObjectMapper.Map<List<ApiResources.ApiResource>, IdentityServer4.Models.ApiResource[]>(apiResources),
                ObjectMapper.Map<List<ApiResources.ApiScope>, IdentityServer4.Models.ApiScope[]>(apiScopes)
            );
        }

        public virtual async Task<IEnumerable<IdentityServer4.Models.IdentityResource>> FindIdentityResourcesByScopeNameAsync(IEnumerable<string> scopeNames)
        {
            var resource = await IdentityResourceRepository.GetListByScopesAsync(scopeNames.ToArray(), includeDetails: true);
            return ObjectMapper.Map<List<IdentityResource>, List<IdentityServer4.Models.IdentityResource>>(resource);
        }

        public virtual async Task<IEnumerable<IdentityServer4.Models.ApiScope>> FindApiScopesByNameAsync(IEnumerable<string> scopeNames)
        {
            var resources = await ApiResourceRepository.GetListByScopesAsync(scopeNames.ToArray(), includeDetails: true);
            return (IEnumerable<IdentityServer4.Models.ApiScope>)ObjectMapper.Map<List<ApiResources.ApiResource>, List<ApiResource>>(resources);
        }

        public virtual async Task<IEnumerable<ApiResource>> FindApiResourcesByScopeNameAsync(IEnumerable<string> scopeNames)
        {
            var resource = await ApiResourceRepository.GetListByScopesAsync(scopeNames.ToArray(), includeDetails: true);
            return ObjectMapper.Map<List<ApiResources.ApiResource>, List<ApiResource>>(resource);
        }

        public virtual async Task<IEnumerable<ApiResource>> FindApiResourcesByNameAsync(IEnumerable<string> apiResourceNames)
        {
            var resource = await ApiResourceRepository.GetListByScopesAsync(apiResourceNames.ToArray(), includeDetails: true);
            return ObjectMapper.Map<List<ApiResources.ApiResource>, List<ApiResource>>(resource);
        }
    }
}
