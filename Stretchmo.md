# Introduction #

THIS IS A WORK IN PROGRESS

Stretchmo, the sequel to Pushmo (, World) and Crashmo continues with almost the same structure as the old games with some changes, introducing compression of LZ10 (0x10) and a new XOR-OUT value.



# QR Data #

**Endianess**: Little endian, Intel, Lowest byte first.

QR Version: 18 (L) - 89x89, 718 bytes

| **Offset** | **Size (bytes)** | **Type** | **Description** |
|:-----------|:-----------------|:---------|:----------------|
| 0x000      | 4                | `uint32` | Magic number (=0x43544E34='4NTC') |
| 0x004      | 4                | `uint32` | CRC-32 over decompressed |
| 0x004      | 4                | `uint32` | Version? (=0x00000001) |
| 0x008      | 4                | `uint32` | Compressed block size |
| 0x00C      | `[]`             | `byte[]` | LZ10 Compressed data (starts with 0x10)|

# Level Data #

**Endianess**: Little endian, Intel, Lowest byte first.

|**Offset**|**Size (bytes)** | **Type** | **Description** |
|:---------|:----------------|:---------|:----------------|
| 0x000    |  4              | `char[4]` | Magic (="4NTC") |
| 0x004    | 4              | `uint32` | CRC-32 over compressed |
|**0xxxx** |**2388**        |          | **Summary**     |

`*`All strings are nullified so last wchar equals 0.

## CRC-32 ##

  * Poly: 0x04C11DB7
  * XOR in: 0x00000000
  * XOR out: 0x03A13848
  * No reflection
