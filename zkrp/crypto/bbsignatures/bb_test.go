package bbsignatures

import (
    "math/big"
    "testing"
)

func TestKeyGen(t *testing.T) {
    kp, _ := Keygen()
    signature, _ := Sign(big.NewInt(42), kp.Privk)
    res, _ := verify(signature, big.NewInt(42), kp.Pubk)
    if res != true {
        t.Errorf("Assert failure: expected true, actual: %t", res)
        t.Fail()
    }
}
