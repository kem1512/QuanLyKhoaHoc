"use client";

import { Grid, TextInput } from "@mantine/core";
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
import { CertificateSelect } from "../Helper/AppSelect";

export default function CertificateHandler({ id }: { id?: number }) {
  const CertificateService = new CertificateClient();

  const { data, mutate } = useSWR(`/api/certificate/${id}`, () =>
    CertificateService.getEntity(id)
  );

  const [certificate, setCertificate] = useState<ICertificateMapping>({
    name: "",
    description: "",
    image: "",
    certificateTypeId: 0,
  });

  useEffect(() => {
    if (data) setCertificate(data);
  }, [data]);

  return (
    <DashboardLayout>
      <Grid>
        <Grid.Col span={{ base: 12, lg: 6 }}>
          <TextInput
            label="Tên Chứng Chỉ"
            placeholder="Nhập Tên Chứng Chỉ"
            value={certificate.name}
            onChange={(e) =>
              setCertificate((prev) => ({ ...prev, name: e.target.value }))
            }
            labelProps={{ style: { marginBottom: 6 } }}
          />
        </Grid.Col>
        <Grid.Col span={{ base: 12, lg: 6 }}>
          <TextInput
            label="Mô Tả"
            placeholder="Nhập Mô Tả"
            value={certificate.description}
            onChange={(e) =>
              setCertificate((prev) => ({
                ...prev,
                description: e.target.value,
              }))
            }
            labelProps={{ style: { marginBottom: 6 } }}
          />
        </Grid.Col>
        <Grid.Col span={{ base: 12, lg: 6 }}>
          <TextInput
            label="Ảnh"
            placeholder="Nhập Ảnh"
            value={certificate.image}
            onChange={(e) =>
              setCertificate((prev) => ({ ...prev, image: e.target.value }))
            }
            labelProps={{ style: { marginBottom: 6 } }}
          />
        </Grid.Col>
        <Grid.Col span={{ base: 12, lg: 6 }}>
          <CertificateSelect
            onChange={(e) =>
              setCertificate((prev) => ({
                ...prev,
                certificateTypeId: e,
              }))
            }
          />
        </Grid.Col>
        <Grid.Col span={12}>
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
              }, `${id ? "Sửa" : "Thêm"} Thành Công`, mutate)
            }
          >
            Xác Nhận
          </ActionButton>
        </Grid.Col>
      </Grid>
    </DashboardLayout>
  );
}
