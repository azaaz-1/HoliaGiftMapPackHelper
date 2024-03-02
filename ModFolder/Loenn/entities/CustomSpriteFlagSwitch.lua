local coreToggle = {}

coreToggle.name = "HoliaGiftMapPackHelper/CustomSpriteFlagSwitch"
coreToggle.depth = 2000

function coreToggle.texture(room, entity)
    local setFalseOnly = entity.setFalseOnly
    local setTrueOnly = entity.setTrueOnly

    if setFalseOnly then
        return "objects/coreFlipSwitch/switch13"

    elseif setTrueOnly then
        return "objects/coreFlipSwitch/switch15"

    else
        return "objects/coreFlipSwitch/switch01"
    end
end

coreToggle.placements = {
    {
        name = "both",
        data = {
            setFalseOnly = false,
            setTrueOnly = false,
            flag = ""
        },
    },
    {
        name = "fire",
        data = {
            setFalseOnly = false,
            setTrueOnly = true,
            flag = ""
        },
    },
    {
        name = "ice",
        data = {
            setFalseOnly = true,
            setTrueOnly = false,
            flag = ""
        },
    }
}

return coreToggle