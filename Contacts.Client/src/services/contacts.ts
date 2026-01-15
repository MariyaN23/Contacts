const API_URL = "/api/contacts"

export const getAllContacts = async () => {
    return await fetch(API_URL)
}

export const deleteContact = async (id: string) => {
    await fetch(`${API_URL}/${id}`, {
        method: 'DELETE',
        headers: {
            'Content-Type': 'application/json',
        }
    })
}
