# Introduction #

(todo)



# QR Data #

**This is true for both FreakyForms and FreakyForms Deluxe**

**Endianess**: Little endian, Intel, Lowest byte first.

QR Version: Not specific

| **Offset** | **Size (bytes)** | **Type** | **Description** |
|:-----------|:-----------------|:---------|:----------------|
| 0x000      | 4                | `char[4]` | Magic (='3DCT') |
| 0x004      | 2                | `uint16` | LZ-10 Decompressed data length |
| 0x006      | 2                | `uint16` | LZ-10 Compressed data length |
| 0x008      | 20               | `byte[20]` | Checksum (unknown) |
| 0x01C      | 4                | `uint32` | Zero padding    |
| 0x020      | `[]`             | `byte[]` | LZ-10 Compressed data |

# Level Data #

| **Offset** | **Size (bytes)** | **Type** | **Description** |
|:-----------|:-----------------|:---------|:----------------|
| 0x000      | 2                | `uint16` | Version (0: Standard, 1: Deluxe) |
| 0x002      | 1                | `byte`   | Magic (0xF0, FreakyForms :-) ) |
| 0x003      | 1                | `byte`   | Version2? (1: Standard, 3: Deluxe) |
| 0x004      | 16               | `byte[16]` | Unknown, ID of somekind |
| 0x014 (standard) | 4                | `uint32` | Counter (unknown) |
| 0x014 (deluxe)| 1                | `byte`   | Counter (unknown) |
| 0x015 (deluxe)| 3                | `byte[3]` | Flags?          |
| 0x018      | 4                | `uint32` | Unknown         |
| 0x01C      | 4                | `uint32` | Zero padding    |
| 0x020      | 5                | `byte[5]` | Zeros?          |
| 0x025      | 11               | `byte[11]` | Unknown         |
| 0x030      | 8                | `uint64` | Some number (length?) |

...Chunks...

| ? | 1 | `byte` | Start of strings (=0x05) |
|:--|:--|:-------|:-------------------------|
| ? | 1 | `byte` | Name of formee length    |
| ? | [.md](.md) | `wchar[]` | Name of formee (UTF-16)  |
| ? | 1 | `byte` | Catchphrase length       |
| ? | [.md](.md) | `wchar[]` | Catchphrase (UTF-16)     |
| ? | 1 | `byte` | Author length            |
| ? | [.md](.md) | `wchar[]` | Author(UTF-16)           |

`*` No null at the end of the strings.

What we should find out:

the data of a formee suppose to be a list of
position+shape+color+rotation
width and height might be added to change proportions...

## Checksum ##

unknown...