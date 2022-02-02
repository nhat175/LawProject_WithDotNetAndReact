﻿using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class CaseTypeManager : ICaseTypeService
    {
        public ICaseTypeDal _caseTypeDal;

        public CaseTypeManager(ICaseTypeDal caseTypeDal)
        {
            _caseTypeDal = caseTypeDal;
        }

        public IResult Add(CaseType caseType)
        {
            _caseTypeDal.Add(caseType);
            return new SuccessResult(Messages.AddedSuccessfuly);
        }

        public IResult ChangeActivity(int id)
        {
            var caseType = _caseTypeDal.Get(ct => ct.CaseTypeId == id);
            caseType.IsActive = !caseType.IsActive;
            var result = Update(caseType);
            if (!result.Success)
                return new ErrorResult(result.Message);
            return new SuccessResult(Messages.ActivityChangedSuccessfuly);
        }

        public IResult Delete(int id)
        {
            var caseType = _caseTypeDal.Get(ct => ct.CaseTypeId == id);
            if (caseType == null)
                return new ErrorResult(Messages.TheItemDoesNotExists);
            _caseTypeDal.Delete(caseType);
            return new SuccessResult(Messages.DeletedSuccessfuly);
        }
        public IDataResult<CaseType> GetById(int id)
        {
            var caseType = _caseTypeDal.GetByIdWithCourtOfficeType(ct => ct.CaseTypeId == id);
            if (caseType == null)
                return new ErrorDataResult<CaseType>(Messages.TheItemDoesNotExists);
            return new SuccessDataResult<CaseType>(caseType, Messages.GetByIdSuccessfuly);
        }

        public IDataResult<List<CaseType>> GetByLicenceIdAndActivity(int licenceId, int isActive)
        {
            if (isActive == 0)
                return new SuccessDataResult<List<CaseType>>(_caseTypeDal.GetAllWithCourtOfficeType(c => c.LicenceId == licenceId && c.IsActive == false), Messages.GetAllSuccessfuly);
            if (isActive == 1)
                return new SuccessDataResult<List<CaseType>>(_caseTypeDal.GetAllWithCourtOfficeType(c => c.LicenceId == licenceId && c.IsActive == true), Messages.GetAllSuccessfuly);
            return new SuccessDataResult<List<CaseType>>(_caseTypeDal.GetAllWithCourtOfficeType(c => c.LicenceId == licenceId), Messages.GetAllSuccessfuly);
        }

        public IResult Update(CaseType caseType)
        {
            _caseTypeDal.Update(caseType);
            return new SuccessResult(Messages.UpdatedSuccessfuly);
        }
    }
}
