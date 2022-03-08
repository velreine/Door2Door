using System.Collections.Immutable;

namespace Door2Door_API.Models;

public class Route
{
    // Use private readonly ImmutableList in combination with public property to ensure the list reference,
    // and items in the list itself cannot be modified once object is constructed.
    private readonly ImmutableList<string> _geometries;
    
    public ImmutableList<string> Geometries => this._geometries;


    public Route(List<string> geometries)
    {
        if (geometries == null) throw new ArgumentNullException(nameof(geometries));
        this._geometries = geometries.ToImmutableList();
    }
}