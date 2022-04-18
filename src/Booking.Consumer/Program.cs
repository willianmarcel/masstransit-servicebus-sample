using Booking.Consumer.Consumers;
using Booking.Core.BuldingBlocks.Bus;
using MassTransit;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<TicketConsumer>();
    x.UsingAzureServiceBus((ctx, cfg) => 
    {
        cfg.Host(builder.Configuration.GetConnectionString("BusConnection"));

        cfg.ReceiveEndpoint(EventBusConstants.OrderTicketQueue, c => {
            c.ConfigureConsumer<TicketConsumer>(ctx);
        });
    });
});

builder.Services.AddScoped<TicketConsumer>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
