<script setup lang="ts">
import DataTable from 'primevue/datatable';
import Column from 'primevue/column';
import Button from 'primevue/button';
import {onMounted} from "vue";
import ContactsModal from "@/components/ContactsModal.vue";
import {stringToDateFormatter} from "@/utils/date-formatter.ts";
import DatePicker from "primevue/datepicker";
import {useContacts} from "@/composables/useContacts.ts";

const {
  contacts,
  isLoading,
  getContacts,
  deleteCurrentContact,
  addNewContact,
  updateCurrentContact,
} = useContacts()

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
          @submit-form="addNewContact"
          :initial-values="{
            name: '',
            mobilePhone: '',
            jobTitle: '',
            birthDate: new Date(),
          }"
      />
    </div>
    <DataTable :value="contacts" :loading="isLoading">
      <Column field="name" header="Name"></Column>
      <Column field="mobilePhone" header="Mobile phone"></Column>
      <Column field="jobTitle" header="Job title"></Column>
      <Column field="birthDate" header="Birth date">
        <template #body="slotProps">
          {{ new Date(slotProps.data.birthDate).toLocaleDateString() }}
        </template>
      </Column>
      <Column field="edit" header="Options">
        <template #body="slotProps">
          <div class="flex gap-1">
            <ContactsModal
                aria-label="Edit"
                icon="pi pi-pencil"
                size="small"
                variant="outlined"
                modal-header="Edit contact"
                @submit-form="(values) => updateCurrentContact(slotProps.data.id, values)"
                :initial-values="{
                  name: slotProps.data.name,
                  mobilePhone: slotProps.data.mobilePhone,
                  jobTitle: slotProps.data.jobTitle,
                  birthDate: stringToDateFormatter(slotProps.data.birthDate)
                }"
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
