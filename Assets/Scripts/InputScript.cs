using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputScript : MonoBehaviour {

	// click tem uma posição x e y
	// clica -> pega a posição (x, y)
	// solta -> pega outra (x, y)
	// clica.y < solta.y (swipe vert) :: limite pro y variar, tipo se deltaY <= 10 ignorar
	// clica.x < solta.x (swipe hori) 

	// deltaY e deltaX
	// if deltaY > deltaX :: swipe vert
	// else if deltaX < deltaY :: swip hor

	// clica <- get tempo
	// solta <- get tempo

	// ou com um limite de tempo :: se o limite explode > "força" solta :: geralmente vai ser um número inteiro 0~inf
	// ou com um limite de distância :: quando a distância explode > verifica o tempo :: geralmente vai ser um numero float 0~inf
	// quando o cara soltar o mouse < pegar a distancia, pegar o tempo, pegar a dist. min e o tempo min.
}
