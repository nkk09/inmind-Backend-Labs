using Swashbuckle.AspNetCore.SwaggerGen;

namespace lab1_nour_kassem.Services;

public class ObjectMapperService
{
    public TDestination Map<TSource, TDestination>(TSource source)
    {
        if (source == null) throw new ArgumentNullException(nameof(source));
        var destination = Activator.CreateInstance<TDestination>();
        var sourceproperties = typeof(TSource).GetProperties();
        var destinationproperties = typeof(TDestination).GetProperties();

        foreach (var destinationproperty in destinationproperties)
        {
            var sourceproperty = sourceproperties
                .FirstOrDefault(
                    x => string.Equals(x.Name, destinationproperty.Name, StringComparison.OrdinalIgnoreCase));
            if (sourceproperty != null)
            {
                if (sourceproperty.PropertyType.IsAssignableTo(destinationproperty.PropertyType))
                {
                    destinationproperty.SetValue(destination, sourceproperty.GetValue(source, null));
                }
                else
                {
                    try
                    {
                        var converted = Convert.ChangeType(sourceproperty.GetValue(source), destinationproperty.PropertyType);
                        destinationproperty.SetValue(destination, converted);
                    }
                    catch
                    {
                        continue;
                    }
                }
            }

        }

        return destination;
    }
}