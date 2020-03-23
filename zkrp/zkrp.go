package main

import (
	"encoding/json"
	"math/big"

	"github.com/aws/aws-lambda-go/lambda"
	"zkrp/bulletproofs"
)

type Request struct {
	Low    int64 `json:"low"`
	High   int64 `json:"high"`
	Secret int64 `json:"secret"`
}

type Response struct {
	Message string `json:"message"`
	Ok      bool   `json:"ok"`
}

func HandleLambdaEvent(request Request) (Response, error) {
	params, _ := bulletproofs.SetupGeneric(request.Low, request.High)

	bigSecret := new(big.Int).SetInt64(int64(request.Secret))

	// Create the zero-knowledge range proof
	proof, _ := bulletproofs.ProveGeneric(bigSecret, params)

	// Encode the proof to JSON
	jsonEncoded, _ := json.Marshal(proof)
	response := Response{
		Message: string(jsonEncoded),
		Ok:      true,
	}

	return response, nil
}

func main() {
	lambda.Start(HandleLambdaEvent)
}
