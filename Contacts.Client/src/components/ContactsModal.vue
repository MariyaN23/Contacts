<script setup lang="ts">
import Dialog from 'primevue/dialog';
import Button from 'primevue/button';
import InputText from 'primevue/inputtext';
import Message from 'primevue/message';
import { Form, type FormSubmitEvent } from '@primevue/forms';
import { ref } from "vue";
import { zodResolver } from '@primevue/forms/resolvers/zod';
import { z } from 'zod';
import { useToast } from "primevue/usetoast";
import { MAX_JOB_TITLE_LENGTH, MAX_NAME_LENGTH } from "@/utils/constants.ts";

defineProps<{
  label?: string
  ariaLabel?: string
  icon: string
  size?: string
  variant?: string
  modalHeader: string
}>()
const emit = defineEmits<{
  (e: 'submit-form', values: any): void
}>()
const visible = ref(false)
const initialValues = ref({
  name: '',
  mobilePhone: '',
  jobTitle: '',
  birthDate: '',
})
const toast = useToast()
const resolver = ref(zodResolver(
    z.object({
      name: z.string().trim()
          .min(1, {message: 'Name is required.'})
          .max(MAX_NAME_LENGTH, {message: `Name should be less than ${MAX_NAME_LENGTH}`}),
      mobilePhone: z.string().trim()
          .min(1, {message: 'Mobile phone is required.'}),
      jobTitle: z.string().trim()
          .min(1, {message: 'Job title is required.'})
          .max(MAX_JOB_TITLE_LENGTH, {message: `Name should be less than ${MAX_JOB_TITLE_LENGTH}`}),
      birthDate: z.string().trim()
          .min(1, {message: 'Birthdate is required.'}),
    }),
))

const onFormSubmit = ({valid, values}: FormSubmitEvent) => {
  if (valid) {
    emit('submit-form', values)
  }
}
</script>

<template>
  <div class="card flex justify-center">
    <Button
        :label="label"
        :aria-label="ariaLabel"
        :icon="icon"
        :size="size"
        :variant="variant"
        @click="visible = true"
    />
    <Dialog
        v-model:visible="visible"
        modal
        :header="modalHeader"
    >
      <Form v-slot="$form" :resolver="resolver" :initialValues="initialValues" @submit="onFormSubmit"
            class="flex flex-col gap-4 w-full sm:w-56">
        <div class="flex flex-col gap-1">
          <InputText name="name" type="text" placeholder="Name" fluid />
          <Message v-if="$form.name?.invalid" severity="error" size="small" variant="simple">
            {{ $form.name.error?.message }}
          </Message>
        </div>
        <div class="flex flex-col gap-1">
          <InputText name="mobilePhone" type="text" placeholder="Mobile phone" fluid />
          <Message v-if="$form.mobilePhone?.invalid" severity="error" size="small" variant="simple">
            {{ $form.mobilePhone.error?.message }}
          </Message>
        </div>
        <div class="flex flex-col gap-1">
          <InputText name="jobTitle" type="text" placeholder="Job title" fluid />
          <Message v-if="$form.jobTitle?.invalid" severity="error" size="small" variant="simple">
            {{ $form.jobTitle.error?.message }}
          </Message>
        </div>
        <div class="flex flex-col gap-1">
          <InputText name="birthDate" type="text" placeholder="Birthdate" fluid />
          <Message v-if="$form.birthDate?.invalid" severity="error" size="small" variant="simple">
            {{ $form.birthDate.error?.message }}
          </Message>
        </div>
        <Button type="submit" severity="secondary" label="Submit" />
      </Form>
    </Dialog>
  </div>
</template>
