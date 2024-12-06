#include <iostream>
#include <string>

// Define the character classes
enum class CharacterClass
{
    Warrior,
    Rogue,
    Mage,
    Wizard,
    Ranger,
    Monk,
    Bard,
    Paladin,
    Cleric
};

// Character base class
class Character
{
    public:
    std::string name;
    CharacterClass characterClass;
    int strength, agility, endurance, intelligence, willpower, speed, luck;

    Character(std::string n, CharacterClass c)
        : name(n), characterClass(c), strength(5), agility(5), endurance(5),
        intelligence(5), willpower(5), speed(5), luck(5) { }

    virtual void PrintCharacterInfo()
    {
        std::cout << "Name: " << name << ", Class: " << static_cast<int>(characterClass) << std::endl;
        std::cout << "Strength: " << strength << ", Agility: " << agility
            << ", Endurance: " << endurance << ", Intelligence: " << intelligence
            << ", Willpower: " << willpower << ", Speed: " << speed
            << ", Luck: " << luck << std::endl;
    }

    virtual ~Character() = default;
};

// Factory for character creation
class CharacterFactory
{
    public:
    static Character* CreateCharacter(const std::string& name, CharacterClass charClass)
    {
        Character* character = new Character(name, charClass);

        // Task for student: Implement factory logic to create character with different stats based on class
        switch (charClass)
        {
        case CharacterClass::Warrior:
            character->strength = 10;
            character->intelligence = 3;

            break;
        case CharacterClass::Rogue:
            character->agility = 10;
            character->luck = 7;
            character->strength = 4;
            break;

        case CharacterClass::Mage:
            character->agility = 4;
            character->intelligence = 8;

            break;
        case CharacterClass::Wizard:
            character->agility = 3;
            character->intelligence = 10;
            break;
        case CharacterClass::Ranger:
            character->speed = 7;
            character->intelligence = 10;
            break;
        case CharacterClass::Monk:
            character->agility = 6;
            character->endurance = 7;
            break;
        case CharacterClass::Bard:
            character->luck = 15;
            character->willpower = 7;
            break;
        case CharacterClass::Paladin:
            character->willpower = 8;

            break;
        case CharacterClass::Cleric:
            character->willpower = 88;
            break;

        default:
            character->strength = 0;
            character->agility = 0;
            break;
        }

return character;
    }
};
// Decorator for adding abilities or modifiers to characters
class CharacterDecorator : public Character
{
protected:
    Character* character;

public:
    CharacterDecorator(Character * c) : Character(c->name, c->characterClass), character(c) { }

virtual void PrintCharacterInfo() override
{
    character->PrintCharacterInfo();
}
};

class SwordDecorator : public CharacterDecorator
{

public:

    SwordDecorator(Character * c) : CharacterDecorator(c) {
    this->character->strength += 1;
}


};
class ShieldDecorator : public CharacterDecorator
{

public:


    ShieldDecorator(Character * c) : CharacterDecorator(c) {
    this->character->endurance += 1;
}


};

class RingDecorator : public CharacterDecorator
{

public:


    RingDecorator(Character * c) : CharacterDecorator(c) {
    this->character->luck += 5;
}


};

// Task for student: Implement a concrete decorator (e.g., EnchantedArmor, SpecialWeapon) to modify character stats

int main()
{
    // Task for student: Create a character using the factory and apply decorators

    CharacterFactory* factory = new CharacterFactory();

    Character* char1 = factory->CreateCharacter("Charles", CharacterClass::Cleric);
    char1->PrintCharacterInfo();

    Character* charMod1 = new SwordDecorator(char1);
    charMod1->PrintCharacterInfo();

    Character* charMod2 = new ShieldDecorator(char1);
    charMod2->PrintCharacterInfo();

    Character* charMod3 = new RingDecorator(char1);
    charMod3->PrintCharacterInfo();
    return 0;
}
