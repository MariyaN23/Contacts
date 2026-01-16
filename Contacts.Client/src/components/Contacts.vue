<script setup lang="ts">
import DataTable from 'primevue/datatable';
import Column from 'primevue/column';
import Button from 'primevue/button';
import type { Contact } from "@/types/Contact.ts";
import { createContact, deleteContact, getAllContacts } from "@/services/contacts.ts";
import { onMounted, ref } from "vue";
import { useToast } from 'primevue/usetoast';
import ContactsModal from "@/components/ContactsModal.vue";
import type { ContactRequest } from "@/types/ContactRequest.ts";

const toast = useToast()
const contacts = ref<Contact[]>([])

const getContacts = async () => {
  try {
    const response = await getAllContacts()
    contacts.value = await response.json()
  } catch (error) {
    toast.add({severity: 'error', summary: 'Failed to fetch contacts', detail: error})
  }
}
const deleteCurrentContact = async (id: string) => {
  try {
    await deleteContact(id)
    await getContacts()
  } catch (error) {
    toast.add({severity: 'error', summary: 'Failed to delete contact', detail: error})
  }
}
const addContact = async (data: ContactRequest) => {
  try {
    await createContact(data)
    await getContacts()
    toast.add({severity: 'success', summary: 'New contact added'})
  } catch (error) {
    toast.add({severity: 'error', summary: 'Error', detail: error})
  }
}
onMounted(async () => {
  await getContacts()
})
</script>

<template>
  <div class="py-14 px-4 space-y-4 max-w-2xl mx-auto">
    <h1 class="font-bold text-2xl text-color">Contacts</h1>
    <div class="flex justify-end">
      <ContactsModal
          label="Add new contact"
          icon="pi pi-plus"
          modal-header="Add new contact"
          @submit-form="addContact"
      />
    </div>
    <DataTable :value="contacts">
      <Column field="name" header="Name"></Column>
      <Column field="mobilePhone" header="Mobile phone"></Column>
      <Column field="jobTitle" header="Job title"></Column>
      <Column field="birthDate" header="Birth date"></Column>
      <Column field="edit" header="Options">
        <template #body="slotProps">
          <div class="flex gap-1">
            <ContactsModal
                aria-label="Edit"
                icon="pi pi-pencil"
                size="small"
                variant="outlined"
                modal-header="Edit contact"
            />
            <Button
                icon="pi pi-trash"
                aria-label="Delete"
                size="small"
                severity="danger"
                variant="outlined"
                @click="deleteCurrentContact(slotProps.data.id)"
            />
          </div>
        </template>
      </Column>
      <template #empty>
        <p class="text-center">No contacts found.</p>
      </template>
    </DataTable>
  </div>
</template>
