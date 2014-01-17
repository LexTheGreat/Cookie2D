Cookie2D
===============
Official site is [Indie Armory](http://indiearmory.com).

Cookie2D is simple and fast game maker (SFGM in short) written in C# based on SFML and all what comes with it.

Features
---------
* SFML Graphics and Input (no audio yet)
* No more WinForms in client, now I am creating SFML renderer window
* Gwen GUI library (it is awesome and I just love it <3 It have everything like WinForms, and with everything I MEAN everything lol)
* SpriteBatch optimized sprite drawing (known for example from XNA)
* Maximally optimized map rendering using vertex arrays and single texture (not implemented anywhere yet, I just added base class)
* Custom smart resource manager, what wont load any texture twice (example how to load texture: Content.Load<Texture>("gui/background". Also, it can load shaders and fonts, but it is useless right now.)
* Very easy-to-orientate code thanks to using abstract classes.
* Easy-To-Use game controller abstract class (contains GameLoop, basic SFML and Gwen loading and timers)
* Console like in Source engine (CS etc) - Only base for now, but it will be also able to handle Lua commands

Controls:
---------
* LCTRL - Open console

Credits
-------
#### Developers
Thomas.

#### Graphics
Oryx.

#### Graphics library
Laurent Gomila