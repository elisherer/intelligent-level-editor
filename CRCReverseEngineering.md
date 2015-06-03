# Introduction #

This page is a cover of Gregory Ewing's article about [Reverse-Engineering a CRC Algorithm](http://www.cosc.canterbury.ac.nz/greg.ewing/essays/CRC-Reverse-Engineering.html)



# Getting started #

So you have a data with CRC and you want to know how the CRC is created?

First you must have a couple of things sorted:
  1. Do you know how many bits it uses?
  1. Can you create custom messages?
  1. Is this actualy a CRC?
  1. Do you know which section of data it checks?

The answer to all of these questions must be Yes inorder to use this technic.

## 1. How many bits ##

This is quite easy. You probably suspected a certain place in the data to be the CRC. It changes a lot for small changes in the data.

  * So if you see 2 bytes changing it would be 16 bits
  * If you see 4 bytes changing it would be 32 bits... and so on...

## 2. Creating messages? ##

This must be possible to continue, because we need the ability to create different messages to see how the CRC changes and to get the polynomial at the end.

## 3. CRC? ##

Using the superposition principle you could check if it's a CRC by creating 3 messages (with CRC), or get 3 already available messages.

Terms:

  * Message 1 : MSG1 = DATA1 + CRC1
  * Message 2 : MSG2 = DATA2 + CRC2
  * Message 3 : MSG3 = DATA3 + CRC3

Do the following:

  1. DATAX = DATA1 xor DATA2
  1. CRCX = CRC1 xor CRC2
  1. DATAY = DATA1 xor DATA3
  1. CRCY = CRC1 xor CRC3
  1. DXY = DATAX xor DATAY
  1. CXY = CRCX xor CRCY
  1. DATA = DATA1 xor DXY
  1. CRC = CRC1 xor CXY

Now check:
  * if (DATA == DATA2 xor DATA3) && (CRC == CRC2 xor CRC3) then it's a CRC.

## 4. Which section? ##

This is very important. This is crucial for you to check your solution.

# Preparation #

If you can create different files and can change a string in them, please create 4 files using the ASCII characters 'o', 'm', 'n', 'k'.
The files must be different only by these letters. All other bytes in the files should be equal (besides the CRC which will be different).

  1. File o = 0x6F = 0110 1111 - (the base file)
  1. File n = 0x6E = 0110 1110
  1. File m = 0x6D = 0110 1101
  1. File k - 0x6B = 0110 1011

So every file is different from the 'o' file only by one bit (which are adjacent)

Make 3 new files
  1. o xor n - on the same place of the letter it should be: 0000 0001
  1. o xor m - on the same place of the letter it should be: 0000 0010
  1. o xor k - on the same place of the letter it should be: 0000 0100
  * All the file will be zeros but that bit and the full crc32.

# Getting the Polynomial #

The CRC algorithm for every bit shifts the crc value and alternately xors it with the polynomial.
You just need to find that bit that got shifted with the polynomial. (that's why I collect two bit shift and not only one)

You do this by rotating left the value and xor it with the next crc value, then you get the polynomial from the CRC.

  * All the 'o xor #' files are `XorIn` and `XorOut` canceled, and the CRC is homogenus (using 0 as both values).

The polynomial should be one of the regular ones...

# Getting a `XorOut` if used #

Now you know the polynomial. Xoring the value you get with the crc on a regulat message will bring you the `XorOut` value.

If that doesn't work you could try running that polynomial with reflection (in & no out, no in & out, no in & no out, in & out), 4 variations over all..

Initial value can also be a factor. Try using 0xffffffff (if crc32) and Zero.