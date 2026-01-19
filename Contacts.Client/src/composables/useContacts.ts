import {useToast} from "primevue/usetoast";
import {createContact, deleteContact, getAllContacts, updateContact} from "@/services/contacts.ts";
import type {ContactRequest} from "@/types/ContactRequest.ts";
import {ref} from "vue";
import type {Contact} from "@/types/Contact.ts";

export function useContacts() {
    const toast = useToast()
    const isLoading = ref(false)
    const contacts = ref<Contact[]>([])

    const getContacts = async () => {
        isLoading.value = true
        try {
            const response = await getAllContacts()
            contacts.value = await response.json()
        } catch (error) {
            toast.add({severity: 'error', summary: 'Failed to fetch contacts', detail: error})
        } finally {
            isLoading.value = false
        }
    }
    const deleteCurrentContact = async (id: string) => {
        try {
            await deleteContact(id)
            await getContacts()
            toast.add({severity: 'info', summary: 'Contact deleted'})
        } catch (error) {
            toast.add({severity: 'error', summary: 'Failed to delete contact', detail: error})
        }
    }
    const addNewContact = async (data: ContactRequest) => {
        try {
            await createContact(data)
            await getContacts()
            toast.add({severity: 'success', summary: 'New contact added'})
        } catch (error) {
            toast.add({severity: 'error', summary: 'Error', detail: error})
        }
    }
    const updateCurrentContact = async (id: string, data: ContactRequest) => {
        try {
            await updateContact(id, data)
            await getContacts()
            toast.add({severity: 'info', summary: 'Contact was updated'})
        } catch (error) {
            toast.add({severity: 'error', summary: 'Error', detail: error})
        }
    }

    return {
        contacts,
        isLoading,
        getContacts,
        deleteCurrentContact,
        addNewContact,
        updateCurrentContact,
    }
}
