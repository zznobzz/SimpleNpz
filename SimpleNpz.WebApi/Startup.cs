﻿namespace SimpleNpz.WebApi;

public class Startup
{
    public Startup(IConfiguration configuration )
    {
        Configuration = configuration;
    }
    public IConfiguration Configuration { get; }
    
}