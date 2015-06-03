# Introduction #

(todo)



# QR Data #

The QR Data is the level data compressed with LZ-10 (Lempel-Ziv)
The magic header is 0x10.

QR Version: Not specific

# Level Data #

**Endianess**: Little endian, Intel, Lowest byte first.

| **Offset** | **Size (bytes)** | **Type** | **Description** |
|:-----------|:-----------------|:---------|:----------------|
| 0x000      |  1               | `byte`   | Magic (=1, any other value will freeze the game) |
| 0x001      |  1               | `byte`   | Background (between 0 and 4, over 4 will freeze the game) |
| 0x002      |  160             | `byte[160]` | Level data      |
| 0x0A2      |  4               | `uint32` | Par time        |
| 0x0A6      |  4               | `uint32` | CRC-32 (CCITT, over 0x00-0xA6) |
| **0x0AA**  | **170**          |          | **Summary**     |

## Level sprites ##

  * 00 - Blank
  * 01 - Sand
  * 02 - Bullet
  * 03 - Bullet covered with sand
  * 04 - Spike Ball
  * 05 - Block (Plain)
  * 06 - Block (Bird)
  * 07 - Block (4-blocks)
  * 08 - Block (Tools)
  * 09 - Amulet
  * 0A - Snake
  * 0B - Skull (Horizontal)
  * 0C - Skull (Vertical)
  * 0D - Fire
  * 0E - Dog (Facing up)
  * 0F - Dog (Facing left)
  * 10 - Dog (Facing right)
  * 11 - Dog (Facing down)
  * 12 - Player
  * 13 - Exit Door
  * 14 - Fly
  * 15 - Fly covered with sand
  * 16 - Hourglass
  * 17 - Hourglass covered with sand
  * 18 - Rockdoor
  * 19 - Coins (collectable)
  * 1A - Coins covered with sand (collectable)
  * 1B - Chalice (collectable)
  * 1C - Chalice covered with sand (collectable)
  * 1D - Bug Chain (collectable)
  * 1E - Bug Chain covered with sand (collectable)
  * 1F - Pyramid Chain (collectable)
  * 20 - Pyramid Chain covered with sand (collectable)
  * 21 - Wings (collectable)
  * 22 - Wings covered with sand (collectable)
  * 23 - Big block top-left (plain)
  * 24 - Big block top-right (plain)
  * 25 - Big block btm-left (plain)
  * 26 - Big block btm-right (plain)
  * 27 - Big block top-left (cat-head)
  * 28 - Big block top-right (cat-head)
  * 29 - Big block btm-left (cat-head)
  * 2A - Big block btm-right (cat-head)
  * 2B - Big block top-left (two figures)
  * 2C - Big block top-right (two figures)
  * 2D - Big block btm-left (two figures)
  * 2E - Big block btm-right (two figures)
  * 2F - TNT
  * 30 - TNT Detonator
  * 31 - Spikes
  * 32 - Pillar top
  * 33 - Pillar

## CRC-32 ##

  * Poly: 0x04C11DB7
  * XOR in: 0xFFFFFFFF
  * XOR out: 0xFFFFFFFF
  * Reflection: In & Out