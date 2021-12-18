﻿using Business.Abstract;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Business;
using Core.Utilities.Helpers;
using Core.Utilities.Results;
using Core.Utilities.Results.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Business.Constants.Messages;

namespace Business.Concrete
{
    public class ImageManager : IImageService
    {
        IImageDal _imageDal;
        public ImageManager(IImageDal imageDal)
        {
            _imageDal = imageDal;
        }

        //[ValidationAspect(typeof(ImageValidator))]
        public IResult Add(IFormFile file, Image carImage)
        {
            var imageResult = FileHelper.Upload(file);
            if (!imageResult.Success)
            {
                return new ErrorResult(imageResult.Message);
            }

            IResult result = BusinessRules.Run(CheckIfImageLimitExceded(carImage));

            if (result != null)
            {
                return result;
            }

            carImage.ImagePath = imageResult.Message;
            carImage.Date = DateTime.Now;
            _imageDal.Add(carImage);
            return new SuccessResult(SuccessMessages.ImageAdded);
        }

        //[ValidationAspect(typeof(ImageValidator))]
        public IResult Update(IFormFile file, Image carImage)
        {
            var isImage = _imageDal.Get(c => c.Id == carImage.Id);
            if (isImage == null)
            {
                return new ErrorResult(ErrorMessages.ImageNotFound);
            }

            var updatedFile = FileHelper.Update(file, isImage.ImagePath);
            if (!updatedFile.Success)
            {
                return new ErrorResult(updatedFile.Message);
            }
            carImage.ImagePath = updatedFile.Message;
            carImage.Date = DateTime.Now;
            _imageDal.Update(carImage);
            return new SuccessResult(SuccessMessages.ImageUpdated);
        }

        public IResult Delete(Image carImage)
        {
            var image = _imageDal.Get(c => c.Id == carImage.Id);
            if (image == null)
            {
                return new ErrorResult(ErrorMessages.ImageNotFound);
            }
            FileHelper.Delete(image.ImagePath);
            _imageDal.Delete(carImage);
            return new SuccessResult(SuccessMessages.ImageDeleted);
        }

        public IDataResult<List<Image>> GetAll()
        {
            if (DateTime.Now.Hour == 7)
            {
                return new ErrorDataResult<List<Image>>(ErrorMessages.MaintenanceTime);
            }
            return new SuccessDataResult<List<Image>>(_imageDal.GetAll(), SuccessMessages.ImagesListed);
        }

        public IDataResult<Image> GetById(int id)
        {

            return new SuccessDataResult<Image>(_imageDal.Get(b => b.Id == id), SuccessMessages.ImageListed);
        }


        private IResult CheckIfImageLimitExceded(Image carImage)
        {
            var result = _imageDal.GetAll(c => c.CarId == carImage.CarId).Count;
            if (result >= 5)
            {
                return new ErrorResult(ErrorMessages.ImageLimitExceded);
            }
            return new SuccessResult();
        }

        public IDataResult<List<Image>> GetImagesByCarId(int carId)
        {
            IResult result = BusinessRules.Run(CheckIfImageNull(carId));

            if (result != null)
            {
                return new ErrorDataResult<List<Image>>(result.Message);
            }

            return new SuccessDataResult<List<Image>>(CheckIfImageNull(carId).Data);
        }

        private IDataResult<List<Image>> CheckIfImageNull(int id)
        {
            try
            {
                string path = @"\images.default.png";
                var result = _imageDal.GetAll(c => c.CarId == id).Any();
                if (!result)
                {
                    List<Image> carImages = new List<Image>();
                    carImages.Add(new Image { CarId = id, ImagePath = path, Date = DateTime.Now });
                    return new SuccessDataResult<List<Image>>(carImages);
                }
            }
            catch (Exception exception)
            {

                return new ErrorDataResult<List<Image>>(exception.Message);
            }

            return new SuccessDataResult<List<Image>>(_imageDal.GetAll(p => p.CarId == id).ToList());
        }
    }
}



