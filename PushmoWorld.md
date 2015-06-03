# Introduction #

THIS IS A WORK IN PROGRESS

Pushmo World, the sequel to Pushmo and Crashmo continues with almost the same structure as the old games with some changes, introducing compression of ZLib (789C) and a new XOR-OUT value.



# QR Data #

**Endianess**: Little endian, Intel, Lowest byte first.

QR Version: 18 (L) - 89x89, 718 bytes

| **Offset** | **Size (bytes)** | **Type** | **Description** |
|:-----------|:-----------------|:---------|:----------------|
| 0x000      | 4                | `uint32` | Magic number (=0x0000068D) |
| 0x004      | 4                | `uint32` | World version (=0x00000001) (The 3DS ver was 0) |
| 0x008      | 4                | `uint32` | Zeros           |
| 0x00C      | `[]`             | `byte[]` | Zlib-789C Compressed data (starts with 0x789C)|

# Level Data #

**Endianess**: Little endian, Intel, Lowest byte first.

|**Offset**|**Size (bytes)** | **Type** | **Description** |
|:---------|:----------------|:---------|:----------------|
| 0x000    |  4              | `char[4]` | Magic (="WATA") |
| 0x004    |  4              | `uint32` | Magic number (0x03000000) |
| 0x008    |  4              | `byte[4]` | Custom CRC-32 (over 0x000C-EOF) |
| 0x00C    |  1              | `byte`   | Zero            |
| 0x00D    | 22              | `wchar[11]` | Author's name`*` (UTF-16) |
| 0x033    | 34              | `wchar[17]` | Level's name`*` (UTF-16) |
| 0x043    |  1              | `byte`   | Zero            |
| 0x044    |  1              | `byte`   | Protection (4=locked, 3=open) |
| 0x045    |  1              | `byte`   | Difficulty      |
| 0x046    | 10              | `byte[10]` | Palette data    |
| 0x050    |  2              | `uint16` | Number of gadgets (4 bytes each) |
| 0x052    |124              | `gadget[31]` | Gadgets array`**` |
| 0x0CD    |512              | `byte[512]` | Level data (Rastered, Nibble-per-pixel, top to bottom) |
| 0x2CD    | 22              | `byte[22]` | Footer (Zeros)  |
|**0x2E4** |**740**          |          | **Summary**     |

`*`All strings are nullified so last wchar equals 0.

`**`All gadgets are sorted by type (The flag will always be first).

## Gadget struct ##

| **Offset** | **Size (bytes)** | **Type** | **Description** |
|:-----------|:-----------------|:---------|:----------------|
| 0x000      | 2                | `uint16` | Position (x = bits 12..16 , y = bits 7..11 **negated**) nibbles are swapped|
| 0x002      | 1                | `byte`   | Type (1 = Flag, 2 = Manhole, 3 = ShiftOut switches, 4 = ShiftIn switches)|
| 0x003      | 1                | `byte`   | Flag`*`         |

`*`Flag:
  * For Manholes & Doors it's the color: 0=red, 1=yellow, 2=blue
  * For Shift switches it's the direction: 0=right, 1=left, 4=down, 5=up
  * Flag and clouds don't use the flag field

Note: There can be only 31 gadgets (1 flag, 10 manholes, 10 in switches, 10 out switches).

## CRC-32 ##

  * Poly: 0x04C11DB7
  * XOR in: 0x00000000
  * XOR out: 0x636A25A4
  * No reflection