import type { ContactRequest } from "@/types/ContactRequest.ts";

const API_URL = "/api/contacts"

export const getAllContacts = async () => {
    return await fetch(API_URL)
}

export const createContact = async (req: ContactRequest)=> {
    const res = await fetch(API_URL, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(req),
    })
    if (!res.ok) {
        throw new Error('Failed to add new contact')
    }

    return await res.json()
}

export const updateContact = async (id: string, req: ContactRequest)=> {
    await fetch(`${API_URL}/${id}`, {
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(req),
    })
}

export const deleteContact = async (id: string) => {
    await fetch(`${API_URL}/${id}`, {
        method: 'DELETE',
        headers: {
            'Content-Type': 'application/json',
        }
    })
}
