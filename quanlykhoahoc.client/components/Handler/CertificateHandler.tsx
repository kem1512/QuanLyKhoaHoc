"use client";

import { Checkbox, SimpleGrid, TextInput } from "@mantine/core";
import DashboardLayout from "../../components/Layout/DashboardLayout/DashboardLayout";
import {
  ICertificateMapping,
  CertificateClient,
  CertificateCreate,
  CertificateUpdate,
} from "../../app/web-api-client";
import { handleSubmit } from "../../lib/helper";
import { useEffect, useState } from "react";
import useSWR from "swr";
import ActionButton from "../../components/Helper/ActionButton";

export default function CertificateHandler({ id }: { id?: number }) {
  const CertificateService = new CertificateClient();

  const { data } = useSWR(`/api/certificate/${id}`, () =>
    CertificateService.getEntity(id)
  );

  const [certificate, setCertificate] = useState<ICertificateMapping>({
    name: "",
  });

  useEffect(() => {
    if (data) setCertificate(data);
  }, [data]);

  return (
    <DashboardLayout>
      <SimpleGrid cols={{ base: 1, lg: 2 }}>
        <TextInput
          label="Tên Chủ Đề"
          placeholder="Nhập Tên Chủ Đề"
          value={certificate.name}
          onChange={(e) =>
            setCertificate((prev) => ({ ...prev, name: e.target.value }))
          }
          labelProps={{ style: { marginBottom: 6 } }}
        />
        <ActionButton
          size="xs"
          action={() =>
            handleSubmit(() => {
              return id
                ? CertificateService.updateEntity(
                    certificate.id,
                    certificate as CertificateUpdate
                  )
                : CertificateService.createEntity(
                    certificate as CertificateCreate
                  );
            }, `${id ? "Sửa" : "Thêm"} Thành Công`)
          }
        >
          Xác Nhận
        </ActionButton>
      </SimpleGrid>
    </DashboardLayout>
  );
}
