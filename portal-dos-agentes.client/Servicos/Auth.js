import { getAccessToken } from "./GetAcessToken";

export async function auth(){
    console.warn("Token inválido ou expirado. tentando renovar...");
        const refreshToken = localStorage.getItem('refreshToken');
        let accessToken = await getAccessToken(refreshToken);
        if(!accessToken) {
            console.warn("Falha ao renovar o token de acesso. Redirecionando para a página de login...");
            return false; // Redireciona para a página de login
        }
        else {
            console.warn("Token de acesso renovado com sucesso. Tentando obter os dados do usuário novamente...");
            return true // Tenta novamente após renovar o token
        }
}