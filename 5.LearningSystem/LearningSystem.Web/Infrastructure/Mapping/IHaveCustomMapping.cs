﻿namespace LearningSystem.Web.Infrastructure.Mapping
{
    using AutoMapper;

    public interface IHaveCustomMapping
    {
        void ConfigureMapping(Profile mapper);
    }
}
