//----------------------------------------------------------------------------------------------------
// ConvexHull2.hpp
//----------------------------------------------------------------------------------------------------

//----------------------------------------------------------------------------------------------------
#pragma once
//----------------------------------------------------------------------------------------------------
#include "Engine/Math/Plane2.hpp"
//----------------------------------------------------------------------------------------------------
#include <vector>

//-forward-declaration--------------------------------------------------------------------------------
struct ConvexPoly2;

//----------------------------------------------------------------------------------------------------
struct ConvexHull2
{
    //------------------------------------------------------------------------------------------------
    // Life-cycle method
    //------------------------------------------------------------------------------------------------

    ConvexHull2();
    explicit ConvexHull2(std::vector<Plane2> const& boundingPlanes);
    explicit ConvexHull2(ConvexPoly2 const& convexPoly);

    //------------------------------------------------------------------------------------------------
    // Mutator
    //------------------------------------------------------------------------------------------------

    void Translate(Vec2 const& offset);
    void Rotate(float degrees, Vec2 const& refPoint = Vec2::ZERO);
    void Scale(float scaleFactor, Vec2 const& refPoint = Vec2::ZERO);

    std::vector<Plane2> m_boundingPlanes; // Unordered collection of planes
};

//----------------------------------------------------------------------------------------------------
struct ConvexPoly2
{
    //------------------------------------------------------------------------------------------------
    // Life-cycle method
    //------------------------------------------------------------------------------------------------

    ConvexPoly2() = delete;
    explicit ConvexPoly2(std::vector<Vec2> const& vertexPosCCW);
    explicit ConvexPoly2(ConvexHull2 const& convexHull);

    //------------------------------------------------------------------------------------------------
    // Accessor
    //------------------------------------------------------------------------------------------------

    int                      GetVertexCount() const;
    std::vector<Vec2> const& GetVertexArray() const;
    bool                     IsValid() const;

    //------------------------------------------------------------------------------------------------
    // Mutator
    //------------------------------------------------------------------------------------------------

    bool AddVertexToEnd(Vec2 const& pos);
    void ClearVertices();
    void Translate(Vec2 const& offset);
    void Rotate(float degrees, Vec2 const& refPoint = Vec2::ZERO);
    void Scale(float scaleFactor, Vec2 const& refPoint = Vec2::ZERO);

protected:
    std::vector<Vec2> m_vertexPos; // Dynamic array of positions
};
