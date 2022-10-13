﻿using AutoMapper;
using GeekShopping.Api.Data.ValueObjects;
using GeekShopping.Api.Model;

namespace GeekShopping.Api.Configuration
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<ProductVO, Product>();
                config.CreateMap<Product, ProductVO>();

                config.CreateMap<CategoryVO, Category>();
                config.CreateMap<Category, CategoryVO>();
            });
            return mappingConfig;
        }
    }
}
