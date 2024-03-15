using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Yomikaze.Domain.Common;

namespace YomiApi.Base
{
    public abstract class CRUDBaseController<T, TKey, TInput, TOutput> : ControllerBase where T : class, IEntity<TKey>
    {
        protected DbContext DbContext { get; set; }
        protected IMapper Mapper { get; set; }

        protected IRepo<T, TKey> Repository { get; set; }

        protected CRUDBaseController(DbContext dbContext, IMapper mapper, IRepo<T, TKey> repository)
        {
            DbContext = dbContext;
            Mapper = mapper;
            Repository = repository;
        }

        [HttpGet]
        public virtual ActionResult<IEnumerable<TOutput>> Get()
        {
            return Ok(Mapper.Map<IEnumerable<TOutput>>(Repository.Query()));
        }


        [HttpGet("{key}")]
        public virtual ActionResult<TOutput> Get(TKey key)
        {
            var entity = Repository.Get(key);
            if (entity == null) return NotFound();
            return Ok(Mapper.Map<TOutput>(entity));
        }

        [HttpPost]
        public virtual ActionResult<TOutput> Post(TInput input)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var entity = Mapper.Map<T>(input);
            Repository.Add(entity);
            return Ok(Mapper.Map<TOutput>(entity));
        }

        [HttpPut("{key}")]
        public virtual ActionResult<TOutput> Put(TKey key, TInput input)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var entityToUpdate = Repository.Get(key);
            if (entityToUpdate == null) return NotFound();
            Mapper.Map(input, entityToUpdate);
            Repository.Update(entityToUpdate);
            return Ok(Mapper.Map<TOutput>(entityToUpdate));
        }

        [HttpDelete("{key}")]
        public virtual ActionResult Delete(TKey key)
        {
            var entity =Repository.Get(key);
            if (entity == null) return NotFound();
            Repository.Delete(entity);
            return Ok();
        }
    }

    public abstract class CRUDBaseController<T, TInput, TOutput> : CRUDBaseController<T, long, TInput, TOutput> where T : class, IEntity<long>
    {
        protected CRUDBaseController(DbContext dbContext, IMapper mapper, IRepo<T, long> repository) : base(dbContext, mapper, repository)
        {
        }
    }
}
