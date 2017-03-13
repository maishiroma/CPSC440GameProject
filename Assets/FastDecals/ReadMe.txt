Fast Decals v1.5.6
==================

Fast Decals allows the game to render a large amount of flat decals - such as blood splatters and blob shadows - at a blazing fast speed, with a single draw call. It also provides an automatic aging mechanism, which slowly fades away old decals when new ones are created. Fast Decals was created mobile devices in mind and is best suited to games with a small world, such as tower defense games etc. Eventhough Fast Decals was created for rendering decals in 3d nothing stops you from using it for 2d tile/sprite rendering.

Version 1.5 now comes with built in texture atlas generation support so you are no longer required to create your own atlases. Just drag and drop your textures and the atlas will be generated automatically. However you can still use your own atlases the same way than before.

Documentation
=============

Documentation is provived as a PDF document inside the FastDecals folder.

Website
=======

See benchmarks, online documentation and more about the Fast Decals at http://www.pmjo.org/fastdecals

Support
=======

Incase you are having problems with Fast Decals, don't hesitate to contact support@pmjo.org

Version history
===============

1.5.6:

- Added a new mode called FIFO. It is a persistent decal mode like Aging but instead of aging alpha channel it replaces the oldest decal when max decal count is exceeded and new decal drawn.
- Aging mode alpha calculation fixes
- Updated the documentation

1.5.5:

- Added an option to select if Local Space or World Space is used for the decals. By using the Local Space you can make decals to follow a parent game object.
- Added a field for changing the texture atlas generation folder
- Added an example scene of using the Local Space option
- Updated the documentation

1.5.4:

- Fixed a regression with ClearDecals method
- Added the ClearDecals method to the documentation

1.5.3:

- Fast Decals undo was rewritten to support newer Unity versions
- Fast Decals shaders were cleaned up for better readability
- Fast Decals package was also uploaded with Unity 5 to avoid compatibility dialog in Asset Store
- Support for older Unity versions (< 4.3.4) was dropped

1.5.2:

- Fixed Windows Phone serialization regression
- Changed example scene decal offsets for better Windows Phone support

1.5.1:

- Replaced old shaders with new ones
- Added Fast Decals convenience menu items to the Component menu
- Added Fast Decals Atlas undo support
- Internal atlas hashing changes
- Internal version management changes
- Added assembly info to the dlls
- Updated documentation

1.5:

- Added a new drag and drop based atlas generation component, FastDecalsAtlas
- Moved some code from the core to the FastDecalsRenderer and added a new renderer, FastDecalsAtlasRenderer
- Fixed rendering with custom texture coordinates when horizontal and vertical tile count is set to 1
- Reduced the amount of required garbage collection
- Commented mesh MarkDynamic out by default since it does not work with the FastDecals if multithreaded rendering is used

1.4.2:

- Library was rebuilt to support Windows Phone

1.4.1:

- Fixed an issue where a small unwanted decal was shown in the world origo when using the Aging mode
- Added ClearDecals function which can be used to clear all currently visible decals in the Aging mode

1.4:

- Memory and performance optimizations
- A new example scene, FakeShadows
- PDF documentation

1.3:

- Initial Asset Store version