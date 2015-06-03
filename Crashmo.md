# Introduction #

Crashmo, the sequel to Pushmo continues with almost the same structure as Pushmo with some changes, introducing compression of LZ-10, typical to Nintendo's games.



# QR Data #

**Endianess**: Little endian, Intel, Lowest byte first.

QR Version: 18 (L) - 89x89, 718 bytes

| **Offset** | **Size (bytes)** | **Type** | **Description** |
|:-----------|:-----------------|:---------|:----------------|
| 0x000      | 4                | `uint32` | Magic number (=0x00000AAD) |
| 0x004      | 4                | `uint32` | Might be version (=0x00000001) |
| 0x008      | 4                | `uint32` | LZ-10 Compressed data length |
| 0x00C      | `[]`             | `byte[]` | LZ-10 Compressed data |

# Level Data #

**Endianess**: Little endian, Intel, Lowest byte first.

| **Offset** | **Size (bytes)** | **Type** | **Description** |
|:-----------|:-----------------|:---------|:----------------|
| 0x000      |  4               | `char[4]` | Magic (="MTUA") |
| 0x004      |  4               | `byte[4]` | Custom CRC-32 (over 0x0008-0x02CC) |
| 0x008      |  4               | `uint32` | Always equals 7 |
| 0x00C      | 16               | `byte[16]` | Zeros           |
| 0x01C      | 22               | `wchar[11]` | Author's name`*` (UTF-16) |
| 0x032      | 34               | `wchar[17]` | Level's name`*` (UTF-16) |
| 0x054      |  1               | `byte`   | Zero            |
| 0x055      |  4               | `uint32` | Difficulty      |
| 0x059      |  7               | `byte[7]` | Some signature (=0x042C0920010000)|
| 0x060      | 10               | `byte[10]` | Palette data    |
| 0x06A      |  6               | `byte[6]` | Zero padding    |
| 0x070      |  4               | `uint32` | Number of gadgets |
| 0x074      | 88               | `gadget[22]` | Gadgets array`**` |
| 0x0CC      | 512              | `byte[512]` | Level data (Rastered, Nibble-per-pixel, bottom to top) |
| 0x2CC      |  1               | `byte`   | Protection (4=locked, 3=open) |
| 0x2CD      |  3               | `byte[3]` | Footer (=0xFAFF0f) |
| **0x2D0**  | **720**          |          | **Summary**     |

`*`All strings are nullified so last wchar equals 0.

`**`All gadgets are sorted by type (The flag will always be first).

## Gadget struct ##

| **Offset** | **Size (bytes)** | **Type** | **Description** |
|:-----------|:-----------------|:---------|:----------------|
| 0x000      | 2                | `uint16` | Position (x = bits 12..16 , y = bits 7..11 **negated**) |
| 0x002      | 1                | `byte`   | Type (1 = Flag, 2 = Manhole, 3 = Shift switches, 4 = Doors, 5 = Cloud)|
| 0x003      | 1                | `byte`   | Flag`*`         |

`*`Flag:
  * For Manholes & Doors it's the color: 0=red, 1=yellow, 2=blue
  * For Shift switches it's the direction: 0=right, 1=left, 4=down, 5=up
  * Flag and clouds don't use the flag field

Note: There can be only 22 gadgets (1 flag, 6 manholes, 6 doors, 4 switches & 5 clouds).

## CRC-32 ##

  * Poly: 0x04C11DB7
  * XOR in: 0x00000000
  * XOR out: 0x45B54367
  * No reflection