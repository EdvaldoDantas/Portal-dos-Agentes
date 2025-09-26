import api from '../src/components/Requisições/Urlapi'
import { auth } from './Auth';

export async function getData(router) {
    try {
        const response = await api.get("api/User/userData", {
            headers: {
                Authorization: `Bearer ${sessionStorage.getItem('accessToken')}`
            }
        });
        if(response.status == 200) {
            console.log("Dados do usuário obtidos com sucesso.");
        }
        const user = response.data;
        return user;
    } catch (error) {
        if (error.response && error.response.status === 401) {
            if(await auth()) {
                console.warn("Tentando obter os dados do usuário novamente após renovar o token...");
                return getData(router); // Tenta novamente após renovar o token
            }
            else {
                console.warn("Falha ao renovar o token de acesso. Redirecionando para a página de login..."); // Redireciona para a página de login
                return false
            }
        }   
        console.error("Error fetching user data:", error);
        throw error;
    }
}