//----------------------------------------------------------------------------------------------------
// Plane2.cpp
//----------------------------------------------------------------------------------------------------

//----------------------------------------------------------------------------------------------------
#include "Engine/Math/Plane2.hpp"
//----------------------------------------------------------------------------------------------------
#include "Engine/Math/MathUtils.hpp"

//----------------------------------------------------------------------------------------------------
/// @param normal Normal vector
/// @param refPosOnPlane Signed Distance (to plane from origin along normal)
//----------------------------------------------------------------------------------------------------
Plane2::Plane2(Vec2 const& normal,
               Vec2 const& refPosOnPlane)
    : m_normal(normal),
      m_distanceFromOrigin(DotProduct2D(normal, refPosOnPlane))
{
}

//----------------------------------------------------------------------------------------------------
/// @brief Get the point on plane nearest to origin(0,0,0)
/// @return NearestPointToOrigin
/// #TODO rename this function to GetNearestPointToOrigin
Vec2 Plane2::GetOriginPoint() const
{
    return m_normal * m_distanceFromOrigin;
}

//----------------------------------------------------------------------------------------------------
float Plane2::GetAltitudeOfPoint(Vec2 const& refPoint) const
{
    float const projectedLength = DotProduct2D(refPoint, m_normal);
    
    return projectedLength - m_distanceFromOrigin;
}

//----------------------------------------------------------------------------------------------------
Vec2 Plane2::GetNearestPoint(Vec2 const& refPoint) const
{
    return refPoint - m_normal * GetAltitudeOfPoint(refPoint);
}
