﻿using Core.Entities;
using Core.Utilities.Results.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Business
{
    public interface IServiceRepository<TEntity> where TEntity : class, IEntity, new()
    {
        IDataResult<List<TEntity>> GetAll();
        IDataResult<TEntity> GetById(int id);
        IResult Add(TEntity entity);
        IResult Update(TEntity entity);
        IResult Delete(TEntity entity);
    }
}