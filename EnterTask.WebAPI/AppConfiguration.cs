using EnterTask.WebAPI.ExceptionMiddleware;

namespace EnterTask.WebAPI
{
    public static class AppConfiguration
    {
        public static IApplicationBuilder ConfigureApp(this IApplicationBuilder builder)
        {
            builder.UseSwagger();
            builder.UseSwaggerUI();

            builder.UseMiddleware<ExceptionHandlingMiddleware>();
            builder.UseHttpsRedirection();

            builder.UseAuthentication();
            builder.UseAuthorization();

            return builder;
        }
    }
}
