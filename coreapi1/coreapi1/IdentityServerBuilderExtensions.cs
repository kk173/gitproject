using IdentityServer4.Models;
using IdentityServer4.Services;
using IdentityServer4.Stores;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace coreapi1
{
    public static class IdentityServerBuilderExtensions
    {
        /// <summary>
        /// 配置Dapper接口和实现(默认使用SqlServer)
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <param name="storeOptionsAction">存储配置信息</param>
        /// <returns></returns>
        public static IIdentityServerBuilder AddDbStore(this IIdentityServerBuilder builder)
        {
            //var options = new DapperStoreOptions();
            //builder.Services.AddSingleton(options);
            //storeOptionsAction?.Invoke(options);
            //builder.Services.AddTransient<IClientStore, SqlServerClientStore>();
            //builder.Services.AddTransient<IResourceStore, SqlServerResourceStore>();
            builder.Services.AddTransient<IProfileService, SelfProfileService>();
            return builder;
        }

    }

    public class UserStore : IUserConsentStore
    {
        public Task<Consent> GetUserConsentAsync(string subjectId, string clientId)
        {
            throw new NotImplementedException();
        }

        public Task RemoveUserConsentAsync(string subjectId, string clientId)
        {
            throw new NotImplementedException();
        }

        public Task StoreUserConsentAsync(Consent consent)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// 金焰的世界
    /// 2018-12-03
    /// 重写授权信息存储
    /// </summary>
    public class SqlServerPersistedGrantStore : IPersistedGrantStore
    {
        private readonly ILogger<SqlServerPersistedGrantStore> _logger;

        public SqlServerPersistedGrantStore(ILogger<SqlServerPersistedGrantStore> logger)
        {
            _logger = logger;
        }

        static List<PersistedGrant> allper = new List<PersistedGrant>();

        /// <summary>
        /// 根据用户标识获取所有的授权信息
        /// </summary>
        /// <param name="subjectId">用户标识</param>
        /// <returns></returns>
        public async Task<IEnumerable<PersistedGrant>> GetAllAsync(string subjectId)
        {
            //using (var connection = new SqlConnection(_configurationStoreOptions.DbConnectionStrings))
            //{
            //    string sql = "select * from PersistedGrants where SubjectId=@subjectId";
            //    var data = (await connection.QueryAsync<Entities.PersistedGrant>(sql, new { subjectId }))?.AsList();
            //    var model = data.Select(x => x.ToModel());

            //    _logger.LogDebug("{persistedGrantCount} persisted grants found for {subjectId}", data.Count, subjectId);
            //    return model;
            //}

            return allper;
        }

        /// <summary>
        /// 根据key获取授权信息
        /// </summary>
        /// <param name="key">认证信息</param>
        /// <returns></returns>
        public async Task<PersistedGrant> GetAsync(string key)
        {
            //using (var connection = new SqlConnection(_configurationStoreOptions.DbConnectionStrings))
            //{
            //    string sql = "select * from PersistedGrants where [Key]=@key";
            //    var result = await connection.QueryFirstOrDefaultAsync<Entities.PersistedGrant>(sql, new { key });
            //    var model = result.ToModel();

            //    _logger.LogDebug("{persistedGrantKey} found in database: {persistedGrantKeyFound}", key, model != null);
            //    return model;
            //}
            return await Task.Run(() => allper.Find(s => s.Key == key));
        }

        /// <summary>
        /// 根据用户标识和客户端ID移除所有的授权信息
        /// </summary>
        /// <param name="subjectId">用户标识</param>
        /// <param name="clientId">客户端ID</param>
        /// <returns></returns>
        public async Task RemoveAllAsync(string subjectId, string clientId)
        {
            //using (var connection = new SqlConnection(_configurationStoreOptions.DbConnectionStrings))
            //{
            //    string sql = "delete from PersistedGrants where ClientId=@clientId and SubjectId=@subjectId";
            //    await connection.ExecuteAsync(sql, new { subjectId, clientId });
            //    _logger.LogDebug("remove {subjectId} {clientId} from database success", subjectId, clientId);
            //}
            allper.RemoveAll(s => s.SubjectId == subjectId && s.ClientId == clientId);
        }

        /// <summary>
        /// 移除指定的标识、客户端、类型等授权信息
        /// </summary>
        /// <param name="subjectId">标识</param>
        /// <param name="clientId">客户端ID</param>
        /// <param name="type">授权类型</param>
        /// <returns></returns>
        public async Task RemoveAllAsync(string subjectId, string clientId, string type)
        {
            //using (var connection = new SqlConnection(_configurationStoreOptions.DbConnectionStrings))
            //{
            //    string sql = "delete from PersistedGrants where ClientId=@clientId and SubjectId=@subjectId and Type=@type";
            //    await connection.ExecuteAsync(sql, new { subjectId, clientId });
            //    _logger.LogDebug("remove {subjectId} {clientId} {type} from database success", subjectId, clientId, type);
            //}
            allper.RemoveAll(s => s.ClientId == clientId && s.SubjectId == subjectId && s.Type == type);
        }

        /// <summary>
        /// 移除指定KEY的授权信息
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task RemoveAsync(string key)
        {
            //using (var connection = new SqlConnection(_configurationStoreOptions.DbConnectionStrings))
            //{
            //    string sql = "delete from PersistedGrants where [Key]=@key";
            //    await connection.ExecuteAsync(sql, new { key });
            //    _logger.LogDebug("remove {key} from database success", key);
            //}
            allper.RemoveAll(s => s.Key == key);
        }

        /// <summary>
        /// 存储授权信息
        /// </summary>
        /// <param name="grant">实体</param>
        /// <returns></returns>
        public async Task StoreAsync(PersistedGrant grant)
        {
            //using (var connection = new SqlConnection(_configurationStoreOptions.DbConnectionStrings))
            //{
            //    //移除防止重复
            //    await RemoveAsync(grant.Key);
            //    string sql = "insert into PersistedGrants([Key],ClientId,CreationTime,Data,Expiration,SubjectId,Type) values(@Key,@ClientId,@CreationTime,@Data,@Expiration,@SubjectId,@Type)";
            //    await connection.ExecuteAsync(sql, grant);
            //}
            allper.Add(grant);
        }
    }
}
