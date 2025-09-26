export function getRoleFromJWT(token) {
  if (!token) return null;

  try {
    const payload = token.split('.')[1]; // pega a parte do meio do JWT
    const decoded = JSON.parse(atob(payload)); // decodifica de Base64 e faz parse do JSON

    return decoded.role || decoded.roles || null; // dependendo de como a role vem
  } catch (e) {
    console.error('Erro ao decodificar o token:', e);
    return null;
  }
}
