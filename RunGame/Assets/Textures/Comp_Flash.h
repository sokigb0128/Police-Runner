#ifndef Comp_Flash_h
#define Comp_Flash

class Component_Flash:public EasyComponent
{

public:

    void init(SPtr<YsTextureSet> _gm);
    void InitFromJson(const json11::Json& jsonObj) override;
    virtual void Update() override;
    virtual void Draw() override;

private:

    YsBillBoard   m_Texture;
    YsMatrix m_Mat;
    float YScaleValue = 1.0f;

};

#endif