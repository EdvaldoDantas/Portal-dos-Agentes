import api from '../src/components/Requisições/Urlapi'

export async function getAccessToken(token) {
    console.warn('Tentando obter novo token de acesso com o refresh token:', token)
    try{
        let response = await api.post('/api/Auth/GetToken',
            {refreshToken : token}
        )
        const newAccessToken = response.data.accessToken
        // Salva o novo token no sessionStorage
        sessionStorage.setItem('accessToken', newAccessToken)
        return newAccessToken
    }
    catch(error)
    {
        console.log(error)
        return null
    }
}