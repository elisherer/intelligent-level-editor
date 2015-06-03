# Introduction #

Pushmo, the 3D puzzle made by Intelligent Systems (with Nintendo) is a great puzzle using QR codes to import/export levels.



# QR Data / Level Data #

**Endianess**: Little endian, Intel, Lowest byte first.

QR Version: 18 (L) - 89x89, 718 bytes

| **Offset** | **Size (bytes)** | **Type** | **Description** |
|:-----------|:-----------------|:---------|:----------------|
| 0x000      |  4               | `uint32` | Magic number (=0x0000068D) |
| 0x004      |  4               | `uint32` | Might be version (=0x00000000) |
| 0x008      |  4               | `byte[4]` | Custom CRC-32   |
| 0x00C      |  1               | `byte`   | Difficulty      |
| 0x00D      |  1               | `byte`   | Always equals 7 |
| 0x00E      | 10               | `byte[10]` | Palette data    |
| 0x018      |  4               | `gadget` | Flag position   |
| 0x01C      | 40               | `gadget[10]` | Pullout switches |
| 0x044      | 40               | `gadget[10]` | Manholes        |
| 0x06C      | 34               | `wchar[17]` | Level's name`*` (UTF-16) |
| 0x08E      | 14               | `byte[14]` | Some signature (=0x000401C44B2A00A0D41308080000) |
| 0x09C      | 20               | `byte[20]` | Garbage         |
| 0x0B0      |  4               | `uint32` | Flags`**`       |
| 0x0B4      | 26               | `byte[26]` | FFs (0xFFFF...) |
| 0x0CE      | 512              | `byte[512]` | Level data (Mip-Mapped, Nibble-per-pixel) |
| **0x2CE**  | **718**          |          | **Summary**     |

`*`All strings are nullified so last wchar equals 0.

`**`Flags (Bit masks):
  * Protected = 0x002
  * Constant = 0x004
  * Uses Manholes = 0x100
  * Uses Switches = 0x200
  * Large = 0x400

## Gadget struct ##

| **Offset** | **Size (bytes)** | **Type** | **Description** |
|:-----------|:-----------------|:---------|:----------------|
| 0x000      | 1                | `byte`   | X Position      |
| 0x001      | 1                | `byte`   | Y Position      |
| 0x002      | 1                | `byte`   | Icon (0x00:Flag, 0x01-0x0A:Switches, 0x0B-0x14:Manholes)|
| 0x003      | 1                | `byte`   | Flag`*`         |

`*`Flag (Two nibbles `[first:second]`):
  * Flag: `[none:none]`
  * Pullout Switches: `[color_num:none]` (color from palette)
  * Manholes: `[color:upsidedown]` (colors: 0-red, 1-blue. 2-yellow, 3-green, 4-purple)

## CRC-32 ##

  * Poly: 0x04C11DB7
  * XOR in: 0x00000000
  * XOR out: 0xD87A2314
  * No reflection