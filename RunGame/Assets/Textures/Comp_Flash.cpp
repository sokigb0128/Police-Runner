#include"main.h"
#include"Comp_Flash.h"
#include "Game/Component/Comp_Common.h"

void Component_Flash::init(SPtr<YsTextureSet> _gm)
{
    m_Texture.SetVertex(0, 0, 0.3, 0.3);
}

void Component_Flash::InitFromJson(const json11::Json & jsonObj)
{
    auto filename = jsonObj["ModelFileName"].string_value();
    auto texture = APP.m_ResStg.LoadTextureSet(filename);

    YsVec3 vPos, vRot, vScale;

    std::array<YsVec3*, 3> tmp = { &vPos, &vRot, &vScale };
    static const std::array<std::string, 3> data =
    {
        "Position", "Rotation", "Scaling"
    };
    for (auto i = 0; i < tmp.size(); i++) {
        tmp[i]->Set(jsonObj[data[i]]);
    }

    // s—ñ‰»
    m_Mat.CreateMove(vPos);
    m_Mat.RotateX_Local(vRot.x);
    m_Mat.RotateY_Local(vRot.y);
    m_Mat.RotateZ_Local(vRot.z);
    m_Mat.Scale_Local(vScale);

    init(texture);
}

void Component_Flash::Update()
{
    YScaleValue += 1.5f;

    if (YScaleValue > 4.5f)
    {
        YScaleValue = 0.3f;
    }

    m_Texture.SetVertex(0 - YScaleValue / 2, 0 - YScaleValue / 2, YScaleValue, YScaleValue);

}

void Component_Flash::Draw()
{
    ShMgr.m_bsAdd.SetState();

    ShMgr.m_FxSh.DrawBillBoard(m_Texture, &m_Mat);

    ShMgr.m_bsNoAlpha.SetState();
}
