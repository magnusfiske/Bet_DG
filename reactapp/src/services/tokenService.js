

export const getToken = async (email, password) => {
    const requestOptions = {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({"email": email,
        "password": password
        })
    }

    const response = await fetch('/api/token', requestOptions)
    if (response.ok) {
        console.log(response);
        return response.json();
    }
    if (response.status === 402) {
        console.log(response);
        throw new Error('Unautorized');
    } else {
        console.log(response);
        throw new Error('Unexpected error')
    }
}

const extractToken = (response) => {
    const jwt = response.body.accessToken;
    console.log(jwt);
}