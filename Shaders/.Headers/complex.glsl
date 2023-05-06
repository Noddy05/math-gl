
float pi = 3.14159265;

vec2 Mult(vec2 a, vec2 b){
	return vec2(a.x * b.x - a.y * b.y, a.x * b.y + a.y * b.x);
}

vec2 Div(vec2 a, vec2 b){
	return vec2(a.x / b.x + a.y * b.y, a.y / b.x - a.x / b.y);
}

vec2 Pow(vec2 a, float exponent){
	float theta = atan(a.y / a.x);
	float r = length(a);
	return vec2(pow(r, exponent) * cos(theta * exponent), pow(r, exponent) * sin(theta * exponent));
}